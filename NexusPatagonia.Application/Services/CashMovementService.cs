using NexusPatagonia.Application.Interfaces;
using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Domain.Exceptions;
using NexusPatagonia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NexusPatagonia.Application.Services
{
    public class CashMovementService : ICashMovementService
    {
        private readonly ICashMovementRepository _repository;
        public CashMovementService(ICashMovementRepository repository) { 
            _repository = repository;
        }

        public async Task<CashMovementDetailDto?> GetByIdAsync(Guid Id)
        { 
            var cashMovement = await _repository.GetByIdAsync(Id);
            return cashMovement;
        }

        public async Task<CashMovementDto> RegisterCashMovement(CashMovementSaveDto cashMovement)
        {
            if (cashMovement.Amount <= 0) throw new BusinessException("El monto debe ser positivo");

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

            await _repository.AddAsync(entity);
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

        public async Task<List<CashMovementDetailDto>> GetAll()
        {
            var results = await _repository.GetAllAsync();
            return results;
        }
    }
}
