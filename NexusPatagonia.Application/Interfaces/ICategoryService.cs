using NexusPatagonia.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace NexusPatagonia.Application.Interfaces
{
    public interface ICategoryService
    {
        public Task<List<CategoryDto>> GetAllAsync();
    }
}
