using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusPatagonia.Application.Interfaces;

namespace NexusPatagonia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> Get()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

    }
}
