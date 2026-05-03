using NexusPatagonia.Application.Interfaces;
using NexusPatagonia.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NexusPatagonia.Application.Services
{
    public class ExcelImportService : IExcelImportService
    {
        private readonly IEnumerable<IExcelStrategy> _strategies;

        public ExcelImportService(IEnumerable<IExcelStrategy> strategies)
        {
            _strategies = strategies;
        }

        public async Task ImportFileAsync(string fileName, Stream stream, DateTime period,Guid companyId)
        {
            var extension = Path.GetExtension(fileName);
            var strategy = _strategies.FirstOrDefault(s => s.CanHandle(extension));

            if (strategy == null)
                throw new NotSupportedException("Formato de archivo no soportado");

            await strategy.ProcessAsync(stream,period,companyId);
        }
    }
}
