using NexusPatagonia.Application.Interfaces;
using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Domain.Exceptions;
using NexusPatagonia.Domain.Interfaces;
using NexusPatagonia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NexusPatagonia.Application.Services
{
    public class CashMovementService : ICashMovementService
    {
        private readonly ICashMovementRepository _cashMovementRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        public CashMovementService(ICashMovementRepository cashMovementRepository,
            ICategoryRepository categoryRepository,
            ISubcategoryRepository subcategoryRepository) { 
            _cashMovementRepository = cashMovementRepository;
            _categoryRepository = categoryRepository;
            _subcategoryRepository = subcategoryRepository;

        }

        public async Task<CashMovementDetailDto?> GetByIdAsync(Guid Id)
        { 
            var cashMovement = await _cashMovementRepository.GetByIdAsync(Id);
            return cashMovement;
        }

        public async Task<CashMovementDto> RegisterCashMovement(CashMovementSaveDto cashMovement)
        {
            if (cashMovement.Amount <= 0) throw new BusinessException("El monto debe ser positivo");

            // Validate category
            var categoryExists = await _categoryRepository.ExistsAsync(cashMovement.CategoryId);

            if (!categoryExists)
                throw new NotFoundException("La categoría especificado no existe");

            if (!string.Equals(cashMovement.SubcategoryId, string.Empty))
            { 
                var subcategoryExists = await _subcategoryRepository.ExistsAsync(cashMovement.SubcategoryId.Value);

                if (!subcategoryExists)
                    throw new NotFoundException("La subcategoría especificada no existe");
            }

            var entity = new CashMovement
            {
                Amount = cashMovement.Amount,
                Date = cashMovement.Date,
                CategoryId = cashMovement.CategoryId,
                SubcategoryId = cashMovement.SubcategoryId,
                Details = cashMovement.Description,
                EmployeeId = cashMovement.EmployeeId,
                Expense = cashMovement.Expense,
                Invoiced = cashMovement.Invoiced
            };

            await _cashMovementRepository.AddAsync(entity);
            return new CashMovementDto {
                Id = entity.Id,
                Amount = cashMovement.Amount,
                Date = cashMovement.Date,
                CategoryId = cashMovement.CategoryId,
                SubcategoryId = cashMovement.SubcategoryId,
                Description = cashMovement.Description,
                EmployeeId = cashMovement.EmployeeId,
                Expense = cashMovement.Expense,
                Invoiced = cashMovement.Invoiced
            };
        }

        public async Task<List<CashMovementDetailDto>> GetAllAsync()
        {
            var results = await _cashMovementRepository.GetAllAsync();
            return results;
        }
    }
}
