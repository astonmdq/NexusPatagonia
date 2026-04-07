using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NexusPatagonia.Application.Interfaces;
using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Requests;

namespace NexusPatagonia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CashMovementController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICashMovementService _cashMovementService;
        public CashMovementController(IMapper mapper, ICashMovementService cashMovementService) {
            _mapper = mapper;
            _cashMovementService = cashMovementService;
        }

        [HttpPost("send-form")]
        public async Task<IActionResult> SendForm([FromBody] CashMovementRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var cashMovementDto = _mapper.Map<CashMovementSaveDto>(request);
                var result = await _cashMovementService.RegisterCashMovement(cashMovementDto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception)
            { 
                return StatusCode(500, "Ocurrió un error interno al procesar la compra.");
            }

            // Aquí puedes procesar la solicitud y realizar las operaciones necesarias
            // Por ejemplo, podrías guardar los datos en una base de datos o realizar alguna lógica de negocio
            // Simulamos un procesamiento asíncrono
            await Task.Delay(1000);
            // Retornamos una respuesta indicando que la operación fue exitosa
            return Ok(new { message = "Formulario recibido correctamente", data = request });
        }

        /// <summary>
        /// Obtiene una compra por su ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var cashMovement = await _cashMovementService.GetByIdAsync(id);

            if (cashMovement == null)
                return NotFound(new { message = $"No se encontró el movimiento con ID {id}"});

            return Ok(cashMovement);
        }

        /// <summary>
        /// Lista todos los movimientos (opcional).
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cashMovements = await _cashMovementService.GetAll();
            return Ok(cashMovements);
        }
    }
}
