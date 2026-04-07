using Microsoft.AspNetCore.Mvc;
using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Interfaces;

namespace NexusPatagonia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")] // Solo administradores
    public class DocumentUploadController : ControllerBase
    {
        private readonly IEnumerable<IPdfProcessingStrategy> _strategies;
        private readonly IPersistenceCoordinator _persistenceCoordinator;

        public DocumentUploadController(IEnumerable<IPdfProcessingStrategy> strategies, IPersistenceCoordinator persistenceCoordinator)
        {
            _strategies = strategies;
            _persistenceCoordinator = persistenceCoordinator;
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

        
    }
}
