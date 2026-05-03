using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Interfaces;
using System.Globalization;
using System.Text.RegularExpressions;
using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

namespace NexusPatagonia.Infrastructure.Services.Strategies
{
    public class TishPdfStrategy : IPdfProcessingStrategy
    {
        public string DocumentType => "Tish";
        private readonly CultureInfo _cultureAr = new CultureInfo("es-AR");

        public async Task<IExtractedData> ProcessAsync(Stream pdfStream)
        {
            try
            {
                using (var document = PdfDocument.Open(pdfStream))
                {
                    var page = document.GetPage(1);

                    var fullContent = string.Join(" ", page.GetWords().Select(w => w.Text));

                    string accountPattern = @"Cuenta:\s*(\d+)";
                    var accountMatch = Regex.Match(fullContent, accountPattern);
                    string accountNumber = accountMatch.Success ? accountMatch.Groups[1].Value : "Not found";

                    string tradeNamePattern = @"Nombre de Fantasía:\s*([^""]+)";
                    var tradeNameMatch = Regex.Match(fullContent, tradeNamePattern);
                    string tradeName = tradeNameMatch.Success ? tradeNameMatch.Groups[1].Value.Trim() : "Not found";

                    string installmentPattern = @"Cuota\s*(\d{2}-\d{4})";
                    var installmentMatch = Regex.Match(fullContent, installmentPattern);
                    string installment = installmentMatch.Success ? installmentMatch.Groups[1].Value : "Not found";

                    string totalPattern = @"TOTAL A PAGAR:\s*\$([\d\.]+(,\d{2})?)";
                    var totalMatch = Regex.Match(fullContent, totalPattern);
                    string totalAmountDue = totalMatch.Success ? totalMatch.Groups[1].Value : "0,00";

                    var result = new TishDto()
                    {
                        Amount = decimal.Parse(totalAmountDue),
                        MunicipalAccount = accountNumber,
                        Company = tradeName,
                        Period = ParsePeriodToDate(installment)
                    };
                    return result;

                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing PDF: {ex.Message}");
            }
            return null;
        }

        private DateTime ParsePeriodToDate(string period)
        {
            // Esperamos formato "YYYY/MM" (ej: 2026/01)
            if (DateTime.TryParseExact(period, "MM/yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                return result;
            }

            // Si falla el parseo, devolvemos la fecha actual por defecto
            return DateTime.Now;
        }
    }
}
