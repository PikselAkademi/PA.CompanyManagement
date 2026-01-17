using Microsoft.EntityFrameworkCore;
using PA.CompanyManagement.AccountingService.Application.DTOs.Requests.Types;
using PA.CompanyManagement.AccountingService.Application.DTOs.Responses.Types;
using PA.CompanyManagement.AccountingService.Application.Repositories.Types;
using PA.CompanyManagement.AccountingService.Domain.Entities.Types;
using PA.CompanyManagement.AccountingService.Infrastructure.Contexts;
using PA.CompanyManagement.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PA.CompanyManagement.AccountingService.Infrastructure.Repositories.Types
{
    public class ExpenseTypeRepository : IExpenseTypeRepository
    {

        private readonly AccountingDBContext _context;

        public ExpenseTypeRepository(AccountingDBContext context)
        {
            _context = context;
        }

        public async Task<ExpenseTypeResponse> CreateAsync(ExpenseTypeCreateRequest request)
        {
            try
            {
                await _context
                    .ExpenseTypes
                    .AddAsync(new ExpenseType
                    {
                        CreatedBy = request.CreatedBy,
                        Name = request.Name,
                        TaxRate = request.TaxRate,
                    });

                await _context.SaveChangesAsync();

                return await _context
                    .ExpenseTypes
                    .AsNoTracking()
                    .Where(x => x.Name == request.Name && x.TaxRate == request.TaxRate)
                    .OrderBy(x => x.CreatedAt)
                    .Select(x => new ExpenseTypeResponse
                    {
                        Id = x.Id,
                        Name = x.Name,
                        TaxRate = x.TaxRate
                    })
                    .LastOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new PAContextAddException("ExpenseType:Create", ex);
            }
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ExpenseTypeResponse>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ExpenseTypeResponse> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<DetailedExpeseTypeResponse> GetDetailedAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ExpenseTypeUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
