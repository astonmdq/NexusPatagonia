using NexusPatagonia.Application.Interfaces;
using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Domain.Exceptions;
using NexusPatagonia.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NexusPatagonia.Application.Services
{
    public class ProfitabilityService : IProfitabilityService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ICashMovementRepository _cashMovementRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IReceiptRepository _receiptRepository;
        private readonly ICheckingAccountRepository _checkingAccountRepository;
        private readonly IMonthlyConceptRepository _monthlyConceptRepository;
        private readonly ITishRepository _tishRepository;
        private readonly IProfitabilityReportRepository _profitabilityReportRepository;
        private readonly IProfitabilityPdfReport _profitabilityPdfReport;

        public ProfitabilityService(ISaleRepository saleRepository, 
            ICashMovementRepository cashMovementRepository, 
            ICompanyRepository companyRepository, 
            IReceiptRepository receiptRepository,
            ICheckingAccountRepository checkingAccountRepository,
            ITishRepository tishRepository,
            IProfitabilityReportRepository profitabilityReportRepository,
            IProfitabilityPdfReport profitabilityPdfReport) 
        {
            _saleRepository = saleRepository;
            _cashMovementRepository = cashMovementRepository;
            _companyRepository = companyRepository;
            _receiptRepository = receiptRepository;
            _checkingAccountRepository = checkingAccountRepository;
            _tishRepository = tishRepository;
            _profitabilityReportRepository = profitabilityReportRepository;
            _profitabilityPdfReport = profitabilityPdfReport;
        }


        public async Task<byte[]> GeneratePdf(ProfitabilityReportDto report)
        { 
            // valido que la compañia exista
            var company = await _companyRepository.GetByIdAsync(report.CompanyId);

            if (company == null)
                throw new NotFoundException($"No se encontro la compañia con id: {report.CompanyId}");

            #region sales
            // proceso facturas de venta
            // recupero regitro de ventas para este periodo y compañia
            var sales = await _saleRepository.GetByPeriodAsync(report.CompanyId, report.Period);

            if (sales == null)
                throw new NotFoundException($"No se encontraron ventas asociadas a la compañia con id: {report.CompanyId} para el período: {report.Period.ToString("dd/MM/yyyy")}");

            var resultA = (sales.SalesInvoice - sales.CreditNote + sales.DebitNote)/decimal.Parse("1.21");
            var resultB = sales.InternalSalesInvoice - sales.InternalSalesCreditNote + sales.InternalSalesDebitNote;
            var resultAB = resultA + resultB;
            var feeFCA = resultA * decimal.Parse("0.04");
            var feeFCB = resultB * decimal.Parse("0.04");
            #endregion

            #region PreviousFee
            var previousFee = await _profitabilityReportRepository.GetPreviousFeeFc(report.CompanyId,report.Period);
            if (previousFee != null)
            {
                throw new NotFoundException("No se encontraron regalías del mes anterior, no es posible generar el reporte");
            }
            var previousFeeFCA = previousFee.Item1;
            var previousFeeFCB = previousFee.Item2;
            #endregion

            #region CashMovements
            var cashMovements = await _cashMovementRepository.GetAmountsByPeriodAsync(report.CompanyId, report.Period);

            #endregion

            #region Receipts
            var receipts = await _receiptRepository.GetAmountByPeriod(report.CompanyId, report.Period);
            #endregion

            #region CheckingAccounts
            var checkingAccounts = await _checkingAccountRepository.GetAmountByPeriod(report.CompanyId, report.Period);
            #endregion
            // proceso compras - iibb - tish

            #region Monthly concept
            var monthlyConcepts = await _monthlyConceptRepository.GetAmountByPeriod(report.CompanyId, report.Period);
            #endregion

            #region Tish
            var tish = await _tishRepository.GetByPeriod(report.CompanyId,report.Period);
            #endregion

            #region SalaryCashMovements
            var salaryCashMovements = await _cashMovementRepository.GetSalaryByPeriodAsync(report.CompanyId, report.Period);
            #endregion

            #region GrossResult
            var grossResult = resultA + resultB - monthlyConcepts - cashMovements
                - (resultA * decimal.Parse("0.04") + resultB * decimal.Parse("0.04")) +
            (previousFee.Item1 + previousFee.Item2) - (receipts + checkingAccounts
            + salaryCashMovements) - report.IIBB - tish;
            #endregion

            #region IncomeTaxes
            var incomeTaxes = (resultA + monthlyConcepts + receipts + checkingAccounts +
                report.IIBB + tish)*decimal.Parse("0,3");
            #endregion


            var reportProfitability = _profitabilityPdfReport.GenerateReport(new ProfitabilityPdfReportDto() {
                CompanyName = company.Name,
                Cuit = company.Cuit,
                ResultA = resultA,
                ResultB = resultB,
                MonthlyConcepts = monthlyConcepts,
                CashMovements = cashMovements,
                FeeFCA = resultA * decimal.Parse("0.04"),
                FeeFCB = resultB * decimal.Parse("0.04"),
                PreviousFeeFCA = previousFee.Item1,
                PreviousFeeFCB = previousFee.Item1,
                Receipts = receipts,
                CheckingAccounts = checkingAccounts,
                SalaryMovements = salaryCashMovements,
                IIBB = report.IIBB,
                Tish = tish,
                GrossResultMonthly = grossResult,
                NetResult = grossResult - incomeTaxes,
                IncomeTax = incomeTaxes,
                Period = report.Period,
                TotalSale = resultA + resultB,
                ProfitabilityPercent = (grossResult-incomeTaxes)/(resultA + resultB)*100
            });
            return reportProfitability;

        }
    }
}
