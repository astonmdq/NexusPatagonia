using Microsoft.Extensions.DependencyInjection;
using NexusPatagonia.Application.Interfaces;
using NexusPatagonia.Domain.Interfaces;
using NexusPatagonia.Infrastructure.Services.Strategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace NexusPatagonia.Application.Services
{
    public class ExcelProcessorFactory : IExcelProcessorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ExcelProcessorFactory(IServiceProvider serviceProvider)
        { 
            _serviceProvider = serviceProvider;
        }

        public IExcelStrategy GetProcessor(string docType)
        {
            return docType.ToLower() switch
            {
                "sales" => _serviceProvider.GetRequiredService<SaleStrategy>(),
                "checkingAccounts" => _serviceProvider.GetRequiredService<CheckingAccountStrategy>(),
                _ => throw new ArgumentException($"El tipo de documento '{docType}' no está soportado.")
            };
        }
    }
}
