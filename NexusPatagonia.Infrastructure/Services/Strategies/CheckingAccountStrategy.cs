using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace NexusPatagonia.Infrastructure.Services.Strategies
{
    public class CheckingAccountStrategy : IExcelStrategy
    {
        private readonly ICheckingAccountRepository _checkingAccountRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public CheckingAccountStrategy(ICheckingAccountRepository checkingAccountRepository,
            IEmployeeRepository employeeRepository)
        { 
            _checkingAccountRepository = checkingAccountRepository;
            _employeeRepository = employeeRepository;
        }

        public bool CanHandle(string fileName)
        {
            return fileName.Contains("cc",StringComparison.OrdinalIgnoreCase);
        }
        public async Task  ProcessAsync(Stream stream, DateTime period, Guid companyId)
        {
            var results = new List<EmployeeCCDto>();
            var regex = new Regex(@"^(\d+)\s*-\s*(.+)");

            using var reader = new StreamReader(stream);
            // Saltamos encabezados si existen
            await reader.ReadLineAsync();

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(line)) continue;

                var columns = line.Split(',');
                var firstCol = columns[0].Trim();

                var match = regex.Match(firstCol);
                if (!match.Success)
                {
                    throw new InvalidDataException($"Lectura rechazada: '{firstCol}'");
                }    
                 
                if (columns.Length < 2) continue;

                string secondColumn = columns[1].Trim();

                int fileId = int.Parse(match.Groups[1].Value);

                // valido que el legajo exista en base de datos
                var employee = await _employeeRepository.GetByFileAsync(fileId);

                // Removemos el $ y cualquier caracter no numérico excepto el separador decimal
                string cleanBalance = secondColumn.Replace("$", "").Trim();

                if (!decimal.TryParse(cleanBalance, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal balance))
                {
                    throw new InvalidDataException($"Error de formato en saldo para el legajo {fileId}.");
                }

                results.Add(new EmployeeCCDto
                {
                    EmployeeId = employee.Id,
                    Balance = balance
                });
            }

            foreach (var row in results)
            {
                await _checkingAccountRepository.AddAsync(new CheckingAccount()
                {
                    EmployeeId = row.EmployeeId,
                    Amount = row.Balance,
                    Period = period
                });
            }
        }
    }
}
