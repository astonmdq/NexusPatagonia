using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Domain.Interfaces;

namespace NexusPatagonia.Infrastructure.Services.Strategies
{
    public class SaleStrategy : IExcelStrategy
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ICompanyRepository _companyRepository;
        public SaleStrategy(ISaleRepository saleRepository, 
            ICompanyRepository companyRepository) { 
            _saleRepository = saleRepository;
            _companyRepository = companyRepository;
        }

        public bool CanHandle(string fileName) => fileName.Contains("ventas", StringComparison.OrdinalIgnoreCase);
        public async Task ProcessAsync(Stream excelStream, DateTime period, Guid companyId)
        { 
            var company = await _companyRepository.GetByIdAsync(companyId);

            if (company == null)
                throw new KeyNotFoundException($"No se encontró la compañia con ID:{companyId}");

            var totalsByFile = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase)
            {
                { "Factura Int. Venta", 0 },
                { "Factura Venta", 0 },
                { "Nota de Crédito", 0 },
                { "Nota de Débito Int. Venta", 0 },
                { "Nota de Crédito Int. Venta", 0 },
                { "Nota de Débito", 0 } // Caso especial consultado
            };

            // 2. Importante: Asegurarse de que el stream esté al inicio
            if (excelStream.CanSeek)
            {
                excelStream.Position = 0;
            }

            using (var reader = new StreamReader(excelStream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    // Separamos por coma (formato CSV)
                    var columns = line.Split(',');

                    // Según el archivo adjunto:
                    // Índice 1: Tipo Documento
                    // Índice 3: Total
                    if (columns.Length >= 4)
                    {
                        string tipoEnArchivo = columns[1].Trim();

                        // Verificamos si este tipo de documento está en nuestro diccionario
                        if (totalsByFile.ContainsKey(tipoEnArchivo))
                        {
                            if (decimal.TryParse(columns[3], out decimal total))
                            {
                                totalsByFile[tipoEnArchivo] = Math.Abs(total);
                            }
                        }
                    }
                }
            }
            await _saleRepository.AddAsync(new Sale()
            {
                CompanyId = companyId,
                CreditNote = totalsByFile["Nota de Crédito"],
                InternalSalesCreditNote = totalsByFile["Nota de Crédito Int. Venta"],
                InternalSalesDebitNote = totalsByFile["Nota de Débito Int. Venta"],
                InternalSalesInvoice = totalsByFile["Factura Int. Venta"],
                SalesInvoice = totalsByFile["Factura Venta"],
                DebitNote = totalsByFile["Nota de Débito"],
                Period = period
            });
            
        }
    }
}
