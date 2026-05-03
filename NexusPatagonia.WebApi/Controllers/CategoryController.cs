using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NexusPatagonia.Application.Interfaces;

namespace NexusPatagonia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        public CategoryController(IMapper mapper, ICategoryService categoryService)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);

        }
    }
}
