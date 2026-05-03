using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace NexusPatagonia.Infrastructure.Services.Reports
{
    public class ProfitabilityPdfReport : IProfitabilityPdfReport
    {
        public byte[] GenerateReport(ProfitabilityPdfReportDto data)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11).FontFamily(Fonts.Verdana));

                    // --- ENCABEZADO ---
                    page.Header().Column(column =>
                    {
                        column.Item().Text("Nexus Patagonia")
                            .SemiBold().FontSize(24).FontColor(Colors.Blue.Darken4).AlignCenter();

                        column.Item().PaddingTop(5).Text(data.CompanyName)
                            .FontSize(16).AlignCenter();

                        column.Item().Text($"CUIT: {data.Cuit}")
                            .FontSize(10).FontColor(Colors.Grey.Medium).AlignCenter();

                        column.Item().PaddingTop(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                    });

                    // --- CONTENIDO ---
                    page.Content().PaddingVertical(20).Column(column =>
                    {
                        column.Item().PaddingBottom(10).Text($"Período: {data.Period:MMMM yyyy}")
                            .Italic().FontSize(12).AlignCenter();

                        column.Item().Table(table =>
                        {
                            // Definición de columnas (60% descripción, 40% valor)
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(2);
                            });

                            // Encabezado de tabla
                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Descripción");
                                header.Cell().Element(CellStyle).AlignRight().Text("Monto");

                                static IContainer CellStyle(IContainer container)
                                {
                                    return container.DefaultTextStyle(x => x.SemiBold())
                                                    .PaddingVertical(5)
                                                    .BorderBottom(1)
                                                    .BorderColor(Colors.Black);
                                }
                            });
                            
                            // Filas de datos (Mapeo manual para asegurar traducción)
                            AddRow(table, "Ingresos por ventas A", data.ResultA);
                            AddRow(table, "Ingresos por ventas B", data.ResultB);
                            AddRow(table, "Compras", data.MonthlyConcepts);
                            AddRow(table, "Gastos sin factura", data.CashMovements);
                            AddRow(table, string.Concat("Costo Fee Mensual facturado Mes", data.Period.ToString("MM/yyyy")), data.FeeFCA);
                            AddRow(table, string.Concat("Costo Fee Mensual no facturado Mes", data.Period.ToString("MM/yyyy")), data.FeeFCB);
                            AddRow(table, string.Concat("Costo Fee Mensual Mes ", data.Period.AddMonths(-1).ToString("MM/yyyy")), data.PreviousFeeFCA);
                            AddRow(table, string.Concat("Costo Fee Mensual Mes ", data.Period.AddMonths(-1).ToString("MM/yyyy")), data.PreviousFeeFCB);
                            AddRow(table, "Sueldos y cargas sociales A", data.Receipts + data.CheckingAccounts);
                            AddRow(table, "Sueldos y cargas sociales B", data.SalaryMovements);
                            AddRow(table, "Costo impuesto ingresos brutos(4%)", data.IIBB);
                            AddRow(table, "TISH", data.Tish);
                            AddRow(table, "Resultado bruto mes",  data.GrossResultMonthly);
                            AddRow(table, "Impuestos las ganancias",data.IncomeTax);
                            AddRow(table, "Resultado Neto", data.NetResult);
                            AddRow(table, "Venta Total",data.TotalSale);
                            AddRow(table, "Rentabilidad sobre ventas", data.ProfitabilityPercent);
                        });
                    });

                    // --- PIE DE PÁGINA ---
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Página ");
                        x.CurrentPageNumber();
                        x.Span(" - Documento generado por Nexus Patagonia");
                    });
                });
            }).GeneratePdf();
        }

        // Método auxiliar para formatear filas
        private void AddRow(TableDescriptor table, string label, decimal value)
        {
            table.Cell().Element(ValueCellStyle).Text(label);
            table.Cell().Element(ValueCellStyle).AlignRight().Text(value.ToString("C2"));

            static IContainer ValueCellStyle(IContainer container)
            {
                return container.PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten3);
            }
        }
    }
}