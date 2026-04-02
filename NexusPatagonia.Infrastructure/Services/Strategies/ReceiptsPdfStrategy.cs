using System.Text.RegularExpressions;
using System.Globalization;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Interfaces;

namespace NexusPatagonia.Infrastructure.Services.Pdf;

public class ReceiptsPdfStrategy : IPdfProcessingStrategy
{
    public string DocumentType => "Receipts";
    private const double DoubleSpaceThreshold = 10.0; // Píxeles para detectar fin de campo horizontal

    public async Task<IExtractedData> ProcessAsync(Stream pdfStream)
    {
        var receipt = new ReceiptDto();
        receipt.EmployeeReceipts = new List<EmployeeReceiptDto>();
        var culture = new CultureInfo("es-AR");
        var periodRegex = new Regex(@"(\d{2})/(\d{4})"); // Captura MM/AAAA dinámicamente

        using (var document = PdfDocument.Open(pdfStream))
        {
            foreach (var page in document.GetPages())
            {
                var words = page.GetWords().ToList();

                // 1. CAPTURA DE ENCABEZADO (Empresa, CUIT y Periodo)
                if (string.IsNullOrEmpty(receipt.CompanyName))
                {
                    var label = words.FirstOrDefault(w => w.Text.Equals("Empresa:", StringComparison.OrdinalIgnoreCase));
                    if (label != null) receipt.CompanyName = ExtractFieldByGap(words, label);
                }

                if (string.IsNullOrEmpty(receipt.Cuit))
                {
                    var label = words.FirstOrDefault(w => w.Text.Equals("CUIT:", StringComparison.OrdinalIgnoreCase));
                    if (label != null)
                    {
                        var rawCuit = ExtractFieldByGap(words, label);
                        receipt.Cuit = Regex.Replace(rawCuit, @"[^\d]", "");
                    } 
                }

                if (receipt.Period == DateTime.MinValue)
                {
                    var periodLabel = words.FirstOrDefault(w => w.Text.Equals("Periodo:", StringComparison.OrdinalIgnoreCase));
                    if (periodLabel != null)
                    {
                        var lineText = string.Join(" ", words
                            .Where(w => Math.Abs(w.BoundingBox.Bottom - periodLabel.BoundingBox.Bottom) < 5)
                            .Select(w => w.Text));

                        var match = periodRegex.Match(lineText);
                        if (match.Success)
                        {
                            receipt.Period = new DateTime(int.Parse(match.Groups[2].Value), int.Parse(match.Groups[1].Value), 1);
                        }
                    }
                }

                // 2. PROCESAMIENTO DE LA TABLA DE EMPLEADOS
                ProcessEmployeeTable(words, receipt, culture);
            }
        }
        return receipt;
    }

    private string ExtractFieldByGap(List<Word> allWords, Word label)
    {
        var lineWords = allWords
            .Where(w => w.BoundingBox.Left > label.BoundingBox.Right
                     && Math.Abs(w.BoundingBox.Bottom - label.BoundingBox.Bottom) < 3)
            .OrderBy(w => w.BoundingBox.Left)
            .ToList();

        var resultWords = new List<string>();
        for (int i = 0; i < lineWords.Count; i++)
        {
            if (i > 0)
            {
                var gap = lineWords[i].BoundingBox.Left - lineWords[i - 1].BoundingBox.Right;
                if (gap > DoubleSpaceThreshold) break; // Doble espacio detectado
            }
            if (lineWords[i].Text.EndsWith(":")) break; // Siguiente etiqueta detectada

            resultWords.Add(lineWords[i].Text);
        }
        return string.Join(" ", resultWords).Trim();
    }

    private void ProcessEmployeeTable(List<Word> words, ReceiptDto receipt, CultureInfo culture)
    {
        var lines = words.GroupBy(w => Math.Round(w.BoundingBox.Bottom, 0))
                         .OrderByDescending(g => g.Key);

        bool isReading = false;

        foreach (var line in lines)
        {
            var lineWords = line.OrderBy(w => w.BoundingBox.Left).ToList();
            if (!lineWords.Any()) continue;

            var firstText = lineWords.First().Text;

            if (firstText == "Legajo") { isReading = true; continue; }
            if (lineWords.Any(w => w.Text.Contains("Total") && w.Text.Contains("General"))) { isReading = false; break; }

            // Si es una fila de empleado (Legajo de 8 dígitos)
            if (isReading && firstText.Length == 8 && long.TryParse(firstText, out _))
            {
                int cuilIdx = lineWords.FindIndex(w => w.Text.Contains("-"));
                if (cuilIdx == -1) continue;

                int count = lineWords.Count;

                var emp = new EmployeeReceiptDto
                {
                    File = firstText,
                    Cuil = Regex.Replace(lineWords[cuilIdx].Text, @"[^\d]", ""),
                    Name = string.Join(" ", lineWords.Skip(1).Take(cuilIdx - 1).Select(w => w.Text)),

                    // Mapeo contable desde el final de la línea hacia la izquierda
                    Net = Parse(lineWords[count - 1].Text, culture),
                    ArtWithholdings = Parse(lineWords[count - 2].Text, culture),
                    ArtHb = Parse(lineWords[count - 3].Text, culture),
                    ArtEarnings = Parse(lineWords[count - 4].Text, culture),
                    Withholdings = Parse(lineWords[count - 5].Text, culture),
                    FamilyAllowance = Parse(lineWords[count - 6].Text, culture),
                    EarningsWithoutDeductions = Parse(lineWords[count - 7].Text, culture),
                    EarningsWithDeductions = Parse(lineWords[count - 8].Text, culture)
                };

                receipt.EmployeeReceipts.Add(emp);
            }
        }
    }

    private decimal Parse(string value, CultureInfo culture)
    {
        if (string.IsNullOrWhiteSpace(value)) return 0;
        string cleanValue = value.Replace("$", "").Trim();

        // Intentamos primero con la cultura provista (ej. es-AR o en-US)
        if (decimal.TryParse(cleanValue, NumberStyles.Number | NumberStyles.AllowCurrencySymbol, culture, out decimal result))
        {
            return result;
        }

        // Si falla, intentamos con InvariantCulture como fallback 
        // (ideal para formatos estándar de sistema como "1,846,965.00")
        if (decimal.TryParse(cleanValue, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
        {
            return result;
        }
        return 0;
    }
}
