using NexusPatagonia.Application.Interfaces;
using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NexusPatagonia.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<CategoryDto>> GetAllAsync()
        { 
            var results = await _repository.GetAllAsync();
            return results;
        }
    }
}
