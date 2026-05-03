using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusPatagonia.Application.Interfaces;
using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Requests;
using static System.Net.Mime.MediaTypeNames;

namespace NexusPatagonia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfitabilityController : ControllerBase
    {
        private IProfitabilityService _profitabilityService;
        public ProfitabilityController(IProfitabilityService profitabilityService)
        {
            _profitabilityService = profitabilityService;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateReport(ReportRequest request)
        {
            string normalizedMonth = request.Month.Trim().ToLower();

            int monthNumber = GetMonthNumber(normalizedMonth);

            if (monthNumber == 0)
                return BadRequest("El nombre del mes proporcionado no es válido");

            try
            {
                DateTime startDate = new DateTime(request.Year, monthNumber, 1);
                var generatedFile = await _profitabilityService.GeneratePdf(new ProfitabilityReportDto()
                {
                    CompanyId = request.CompanyId,
                    IIBB = request.IIBB,
                    Period = new DateTime(request.Year, monthNumber, 1),
                });

                return Ok(generatedFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int GetMonthNumber(string month)
        {
            return month switch
            {
                "enero" => 1,
                "febrero" => 2,
                "marzo" => 3,
                "abril" => 4,
                "mayo" => 5,
                "junio" => 6,
                "julio" => 7,
                "agosto" => 8,
                "septiembre" => 9,
                "octubre" => 10,
                "noviembre" => 11,
                "diciembre" => 12,
                _ => 0
            };
        }
    }
}
