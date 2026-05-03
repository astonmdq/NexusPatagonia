using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusPatagonia.Application.Interfaces;
using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Interfaces;

namespace NexusPatagonia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DocumentUploadController : ControllerBase
    {
        private readonly IEnumerable<IPdfProcessingStrategy> _strategies;
        private readonly IPersistenceCoordinator _persistenceCoordinator;
        private readonly IExcelImportService _excelImportService;

        public DocumentUploadController(IEnumerable<IPdfProcessingStrategy> strategies, 
            IPersistenceCoordinator persistenceCoordinator,
            IExcelImportService excelImportService)
        {
            _strategies = strategies;
            _persistenceCoordinator = persistenceCoordinator;
            _excelImportService = excelImportService;
        }

        [HttpPost("upload-pdf")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadPdf(IFormFile file, [FromQuery] string docType)
        {
            if (file == null || file.Length == 0) return BadRequest("Archivo no válido.");
            var strategy = _strategies.FirstOrDefault(s =>
            s.DocumentType.Equals(docType, StringComparison.OrdinalIgnoreCase));

            if (strategy == null)
                return BadRequest($"No se encontró una estrategia para el tipo de documento: {docType}");

            try
            { 
                using var stream = file.OpenReadStream();

                IExtractedData extractedData = await strategy.ProcessAsync(stream);

                await _persistenceCoordinator.SaveAsync(extractedData);


                return Ok(new {
                    message = "Procesado exitosamente",
                    FileName = file.FileName,
                    Type = docType
                });
            }
            catch (Exception ex)
            {
                // Aquí capturarás los errores de validación de CUIT que programamos
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadExcel(IFormFile file, [FromQuery] string docType,DateTime period, Guid companyId)
        {
            try
            {
                using var stream = file.OpenReadStream();
                await _excelImportService.ImportFileAsync(file.FileName, stream, period, companyId);
                return Ok(new
                {
                    message = "Procesado exitosamente",
                    FileName = file.FileName,
                    Type = docType
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
