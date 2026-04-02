using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Interfaces;
using System.Globalization;
using System.Text.RegularExpressions;
using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

namespace NexusPatagonia.Infrastructure.Services.Strategies
{
    public class UthgraPdfStrategy : IPdfProcessingStrategy
    {
        public string DocumentType => "Uthgra";

        public async Task<IExtractedData> ProcessAsync(Stream pdfStream)
        {
            var results = new UthgraDto { UthgraDetails = new List<UthgraDetailDto>() };
            var cultureAr = new CultureInfo("es-AR");

            using (var document = PdfDocument.Open(pdfStream))
            {
                foreach (var page in document.GetPages())
                {
                    // Extraemos las líneas de la página actual solamente
                    string pageText = ContentOrderTextExtractor.GetText(page);
                    var lines = pageText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(l => l.Trim())
                                        .ToArray();

                    // 1. Extraer Período (solo si no se encontró antes)
                    if (results.Period == null)
                    {
                        results.Period = FindPeriod(lines);
                    }

                    // 2. Extraer Concepto
                    string concept = FindConcept(lines);

                    // 3. Extraer Total Depositado (buscando por proximidad a la etiqueta)
                    decimal? total = FindTotalWithBuffer(lines, cultureAr);

                    if (!string.IsNullOrEmpty(concept) && total.HasValue)
                    {
                        // Evitamos duplicados si la página tiene dos talones iguales
                        if (!results.UthgraDetails.Any(d => d.Description == concept && d.Amount == total.Value))
                        {
                            results.UthgraDetails.Add(new UthgraDetailDto
                            {
                                Description = concept,
                                Amount = total.Value
                            });
                        }
                    }
                }
            }

            return results;
        }

        private string FindConcept(string[] lines)
        {
            // Lista de conceptos prioritarios para identificar la boleta
            string[] targetConcepts = {
                "Contribución Solidaria S. Carlos de Bariloche",
                "Fondo Convenio F.E.H.G.R.A.",
                "SEGURO DE VIDA Y SEPELIO",
                "Cuota Sindical"
            };

            foreach (var concept in targetConcepts)
            {
                if (lines.Any(l => l.Contains(concept, StringComparison.OrdinalIgnoreCase)))
                {
                    return concept;
                }
            }
            return null;
        }

        private decimal? FindTotalWithBuffer(string[] lines, CultureInfo culture)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                // Buscamos la línea que contiene la etiqueta del total
                if (lines[i].Contains("TOTAL DEPOSITADO", StringComparison.OrdinalIgnoreCase))
                {
                    // Revisamos la línea actual y la siguiente (por si el texto se mezcló arriba o abajo)
                    for (int j = 0; j <= 1; j++)
                    {
                        if (i + j >= lines.Length) continue;

                        string lineToSearch = lines[i + j];

                        // Expresión regular mejorada:
                        // Busca montos con formato argentino (puntos para miles, coma para decimales)
                        // Ejemplo: 1.564.227,18
                        var matches = Regex.Matches(lineToSearch, @"\d{1,3}(\.\d{3})*,\d{2}");

                        if (matches.Count > 0)
                        {
                            // En UTHGRA, si hay dos números (como "0,00 1.564.227,18"), 
                            string exactAmount = matches.Last().Value;
                            return ParseDecimal(exactAmount, culture);
                        }
                    }
                }
            }
            return null;
        }

        public decimal ParseDecimal(string input, CultureInfo culture)
        {
            // Limpiamos caracteres no numéricos excepto separadores
            string cleaned = Regex.Replace(input, @"[^\d.,]", "");

            if (decimal.TryParse(cleaned, NumberStyles.Number | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, culture, out decimal result))
            {
                return result;
            }
            return 0;
        }

        private DateTime? FindPeriod(string[] lines)
        {
            foreach (var line in lines)
            {
                if (line.Contains("Mes:", StringComparison.OrdinalIgnoreCase) && line.Contains("Año:", StringComparison.OrdinalIgnoreCase))
                {
                    return ParseUthgraPeriod(line);
                }
            }
            return null;
        }

        private DateTime? ParseUthgraPeriod(string periodLine)
        {
            var months = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                {"Enero", 1}, {"Febrero", 2}, {"Marzo", 3}, {"Abril", 4}, {"Mayo", 5}, {"Junio", 6},
                {"Julio", 7}, {"Agosto", 8}, {"Septiembre", 9}, {"Octubre", 10}, {"Noviembre", 11}, {"Diciembre", 12}
            };

            var match = Regex.Match(periodLine, @"Mes:\s*(\w+)\s*/\s*Año:\s*(\d{4})", RegexOptions.IgnoreCase);

            if (match.Success)
            {
                string monthName = match.Groups[1].Value.Trim();
                if (months.TryGetValue(monthName, out int month) && int.TryParse(match.Groups[2].Value, out int year))
                {
                    return new DateTime(year, month, 1);
                }
            }
            return null;
        }

        // Mantenemos este método por compatibilidad de interfaz, aunque la lógica principal se movió a ProcessAsync
        public List<string> ExtractTextFromPdf(Stream pdfStream)
        {
            var allLines = new List<string>();
            using (var document = PdfDocument.Open(pdfStream))
            {
                foreach (var page in document.GetPages())
                {
                    string pageText = ContentOrderTextExtractor.GetText(page);
                    allLines.AddRange(pageText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries));
                }
            }
            return allLines;
        }
    }
}