using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Interfaces;
using System.Globalization;
using System.Text.RegularExpressions;
using UglyToad.PdfPig;

namespace NexusPatagonia.Infrastructure.Services.Strategies
{
    public class CPdfStrategy : IPdfProcessingStrategy
    {
        public string DocumentType => "C";

        public async Task<IExtractedData> ProcessAsync(Stream pdfStream)
        {
            var results = new ConceptDto();
            results.ConceptsDetails = new List<ConceptDetailDto>();

            string firstPeriod = null;
            string businessName = null;
            string taxId = null;

            using (var document = PdfDocument.Open(pdfStream))
            {
                var firstPage = document.GetPage(1);
                var headLines = firstPage.GetWords().GroupBy(w => w.BoundingBox.Bottom).OrderByDescending(g => g.Key).ToList();

                businessName = string.Join(" ", headLines.First().OrderBy(w => w.BoundingBox.Left).Select(w => w.Text));
                var splitPeriodRegex = new Regex(@"^(\d{2})\s+(\d{4})");
                var cuitRegex = new Regex(@"\d{2}-\d{8}-\d");
                foreach (var word in firstPage.GetWords())
                {
                    var match = cuitRegex.Match(word.Text);
                    if (match.Success)
                    {
                        taxId = match.Value.Replace("-","");
                        break;
                    }
                }
                results.CompanyName = businessName;
                results.Cuit = taxId;

                foreach (var page in document.GetPages())
                {
                    var lines = page.GetWords().GroupBy(w => w.BoundingBox.Bottom).OrderByDescending(g => g.Key);

                    foreach (var line in lines)
                    {
                        var words = line.OrderBy(w => w.BoundingBox.Left).ToList();
                        if (words.Count < 10) continue;
                        string fullLineText = string.Join(" ", words.Select(w => w.Text)).Trim();

                        var matchPeriod = splitPeriodRegex.Match(fullLineText);
                        string periodCell = words[0].Text;

                        if (matchPeriod.Success)
                        {
                            string month = matchPeriod.Groups[1].Value;
                            string year = matchPeriod.Groups[2].Value;
                            string normalizedPeriod = $"{month}/{year}";
                            
                            if (firstPeriod == null)
                            {
                                firstPeriod = normalizedPeriod;
                                results.Period = new DateTime(int.Parse(year), int.Parse(month), 1);
                            }

                            // 2. Si es el período que buscamos, extraemos la data
                            if (normalizedPeriod == firstPeriod)
                            {
                                // 1. Obtener los montos desde el final (Neto es el antepenúltimo antes de los ceros/iva)
                                // Según tu ejemplo, el Neto es palabras[count - 8]
                                decimal detectedNet = ParseDecimal(words[words.Count - 8].Text, CultureInfo.InvariantCulture);
                                decimal detectedNonTaxable = ParseDecimal(words[words.Count - 6].Text, CultureInfo.InvariantCulture);

                                // 2. Extraer el bloque del concepto (saltando el periodo "11 2025")
                                // Tomamos desde el índice 2 hasta el índice que precede a los montos (Count - 8)
                                var segmentConcepto = words.Skip(2).Take(words.Count - 10).Select(p => p.Text).ToList();

                                // 3. Limpiar el código (002, 003, etc) para quedarnos solo con la descripción
                                // El código suele ser el primer elemento del segmento
                                string cleanDescription = string.Join(" ", segmentConcepto.Skip(1));

                                results.ConceptsDetails.Add(new ConceptDetailDto
                                {
                                    Code = segmentConcepto.FirstOrDefault() ?? "",
                                    Concept = cleanDescription,
                                    Net = detectedNet,
                                    NotTaxed = detectedNonTaxable
                                });
                            }
                            else 
                            {
                                return results;
                            }
                        }
                    }
                }
            }
            return results;

        }

        private bool IsValidPeriod(string text) => System.Text.RegularExpressions.Regex.IsMatch(text, @"^\d{2}/\d{4}$");

        private decimal ParseDecimal(string text, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(text) || text == "0.00") return 0;
            // Limpieza de caracteres extraños y parseo
            decimal.TryParse(text.Replace("$", "").Trim(), NumberStyles.Any, culture, out decimal result);
            return result;
        }
    }
}
