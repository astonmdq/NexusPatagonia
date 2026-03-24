using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NexusPatagonia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // Solo administradores
    public class DocumentUploadController : ControllerBase
    {
        private readonly ServiceTypeA _serviceA;
        private readonly ServiceTypeB _serviceB;
        // ... otros servicios

        public DocumentUploadController(ServiceTypeA serviceA, ServiceTypeB serviceB)
        {
            _serviceA = serviceA;
            _serviceB = serviceB;
        }

        [HttpPost("upload-pdf")]
        public async Task<IActionResult> UploadPdf(IFormFile file, [FromQuery] string docType)
        {
            if (file == null || file.Length == 0) return BadRequest("Archivo no válido.");

            using var stream = file.OpenReadStream();

            switch (docType.ToLower())
            {
                case "maxirest":
                    await _serviceA.ProcessAsync(stream);
                    break;
                case "cucina":
                    await _serviceB.ProcessAsync(stream);
                    break;
                // ... otros casos
                default:
                    return BadRequest("Tipo de documento desconocido.");
            }

            return Ok("Archivo procesado y guardado en la base de datos.");
        }
    }
}
