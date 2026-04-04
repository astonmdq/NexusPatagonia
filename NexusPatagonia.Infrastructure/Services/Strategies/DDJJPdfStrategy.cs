using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Interfaces;
using System.Globalization;
using System.Text.RegularExpressions;
using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

namespace NexusPatagonia.Infrastructure.Services.Strategies
{
    public class DDJJPdfStrategy : IPdfProcessingStrategy
    {
        public string DocumentType => "DDJJ";
        private readonly CultureInfo _cultureAr = new CultureInfo("es-AR");

        public async Task<IExtractedData> ProcessAsync(Stream pdfStream)
        {
            var result = new DDJJDto();
            var targetCodes = new[] { "351", "301", "360", "352", "302", "312", "028" };

            using (var document = PdfDocument.Open(pdfStream))
            {
                // Generalmente la información de cabecera y montos está en la primera página
                var page = document.GetPage(1);
                string fullText = ContentOrderTextExtractor.GetText(page);
                var lines = fullText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                                    .Select(l => l.Trim())
                                    .ToArray();

                // 1. Extraer CUIT y Razón Social (Cabecera)
                result.Cuit = ExtractRegex(fullText, @"C\.U\.I\.T\.\s*([\d-]+)");
                result.BusinessName = ExtractBusinessName(lines);

                // 2. Extraer Período (MM/AAAA)
                var periodMatch = Regex.Match(fullText, @"(\d{2}/\d{4})");
                if (periodMatch.Success)
                    result.Period = ParsePeriod(periodMatch.Value);

                foreach (var line in lines)
                {
                    var matches = Regex.Matches(line, @"(\d{3})\s*[\-\.]?\s*(.*?)\s*(\d{1,3}(?:\.\d{3})*,\d{2})");

                    foreach (Match match in matches)
                    {
                        string code = match.Groups[1].Value;

                        if (targetCodes.Contains(code))
                        {
                            result.Details.Add(new F931DetailDto
                            {
                                Code = code,
                                // Limpiamos la descripción de posibles restos de otros códigos
                                Description = match.Groups[2].Value.Trim().Trim('-', '.'),
                                Amount = ParseDecimal(match.Groups[3].Value, _cultureAr)
                            });
                        }
                    }
                }
            }
            return result;
        }

        private string ExtractDescription(string line, string code)
        {
            // Eliminamos el código del principio y el monto del final para quedarnos con el texto
            // Ejemplo entrada: "351 Contribuciones de Seguridad Social 15.069.635,49"

            // 1. Quitar el código inicial
            string description = Regex.Replace(line, $"^{code}", "").Trim();

            // 2. Quitar el monto final (el patrón de moneda al final de la línea)
            // 2. Removemos el guion medio, espacios o puntos que queden al inicio 
            // Esta es la Regex que solicitaste: ^[ \-\.\s]+
            description = Regex.Replace(description, @"^[ \-\.\s]+", "").Trim();

            // 3. Quitamos el monto final (patrón de moneda al final de la línea)
            // Buscamos el último bloque que parece dinero (ej: 15.069.635,49)
            description = Regex.Replace(description, @"\s*[\d.]*,\d{2}$", "").Trim();

            return description;
        }

        private decimal ExtractLastDecimal(string input)
        {
            // Busca el último patrón de moneda en la línea (ej: 15.069.635,49)
            var matches = Regex.Matches(input, @"[\d.]*,\d{2}");
            if (matches.Count > 0 && decimal.TryParse(matches.Last().Value, NumberStyles.Currency, _cultureAr, out decimal val))
                return val;
            return 0;
        }

        private string ExtractRegex(string input, string pattern) =>
            Regex.Match(input, pattern)?.Groups[1].Value.Trim();

        private DateTime? ParsePeriod(string dateStr) =>
            DateTime.TryParseExact(dateStr, "MM/yyyy", null, DateTimeStyles.None, out var date) ? date : (DateTime?)null;

        private string ExtractBusinessName(string[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                // El formulario tiene el campo "Contribuyente:" o "Apellido y Nombre o Razón Social:"
                if (lines[i].Contains("Contribuyente:", StringComparison.OrdinalIgnoreCase) ||
                    lines[i].Contains("Razón Social:", StringComparison.OrdinalIgnoreCase))
                {
                    // Si el nombre está en la misma línea después del ":" o en la siguiente
                    var parts = lines[i].Split(':');
                    if (parts.Length > 1 && !string.IsNullOrWhiteSpace(parts[1]))
                        return parts[1].Trim();

                    if (i + 1 < lines.Length)
                        return lines[i + 1].Trim();
                }
            }
            return string.Empty;
        }

        public decimal ParseDecimal(string input, CultureInfo culture)
        {
            if (decimal.TryParse(input, NumberStyles.Currency, culture, out decimal result))
            {
                return result;
            }
            return 0;
        }
    }
}
