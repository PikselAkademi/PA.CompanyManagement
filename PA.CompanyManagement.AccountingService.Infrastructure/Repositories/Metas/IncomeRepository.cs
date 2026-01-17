using Microsoft.EntityFrameworkCore;
using PA.CompanyManagement.AccountingService.Application.DTOs.Requests.Metas;
using PA.CompanyManagement.AccountingService.Application.DTOs.Responses.Metas;
using PA.CompanyManagement.AccountingService.Application.Repositories.Metas;
using PA.CompanyManagement.AccountingService.Domain.Entities.Metas;
using PA.CompanyManagement.AccountingService.Infrastructure.Contexts;
using PA.CompanyManagement.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PA.CompanyManagement.AccountingService.Infrastructure.Repositories.Metas
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly AccountingDBContext _context;

        public IncomeRepository(AccountingDBContext context)
        {
            _context = context;
        }


        public async Task<IncomeResponse> CreateAsync(IncomeCreateRequest request)
        {
            try
            {
                var income = new Income
                {
                    Id = Guid.NewGuid(),
                    Amount = request.Amount,
                    Completed = request.Completed,
                    CreatedBy = request.CreatedBy,
                    Description = request.Description,
                    IncomeDate = request.IncomeDate,
                    Title = request.Title,
                    TypeId = request.TypeId,
                };

                await _context.Incomes.AddAsync(income);
                await _context.SaveChangesAsync();

                var type = await _context
                    .IncomeTypes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.TypeId);

                return new IncomeResponse
                {
                    Id = income.Id,
                    Amount = income.Amount,
                    Completed = income.Completed,
                    Description = income.Description,
                    IncomeDate = income.IncomeDate,
                    Title = income.Title,
                    TaxRate = type?.TaxRate,
                    TypeName = type?.Name
                };
            }
            catch (Exception ex)
            {
                throw new PAContextAddException("Income:Create", ex);
            }
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<MinimalIncomeResponse>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<MinimalIncomeResponse>> GetAllAsync(Guid expenseTypeId)
        {
            throw new NotImplementedException();
        }

        public Task<IncomeResponse> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<DetailedIncomeResponse> GetDetailedAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task PatchAsync(IncomePatchRequest request)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IncomeUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
