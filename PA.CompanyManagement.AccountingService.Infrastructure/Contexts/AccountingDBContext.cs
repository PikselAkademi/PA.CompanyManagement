using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PA.CompanyManagement.AccountingService.Domain.Entities.Metas;
using PA.CompanyManagement.AccountingService.Domain.Entities.Types;
using PA.CompanyManagement.Core.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace PA.CompanyManagement.AccountingService.Infrastructure.Contexts
{
    public class AccountingDBContext : DbContext
    {
        private readonly IConfiguration? _conf;
        private readonly ICurrentUser? _currentUser;

        public AccountingDBContext() { }
        public AccountingDBContext(DbContextOptions<AccountingDBContext> options) : base(options) { }

        public AccountingDBContext(IConfiguration conf) => _conf = conf;
        public AccountingDBContext(DbContextOptions<AccountingDBContext> options, IConfiguration conf)
            : base(options) => _conf = conf;

        public AccountingDBContext(ICurrentUser currentUser) => _currentUser = currentUser;
        public AccountingDBContext(DbContextOptions<AccountingDBContext> options, ICurrentUser currentUser)
            : base(options) => _currentUser = currentUser;

        public AccountingDBContext(IConfiguration conf, ICurrentUser currentUser)
        {
            _conf = conf;
            _currentUser = currentUser;
        }
        public AccountingDBContext(DbContextOptions<AccountingDBContext> options,
            IConfiguration conf,
            ICurrentUser currentUser) : base(options)
        {
            _conf = conf;
            _currentUser = currentUser;
        }

        public DbSet<IncomeType> IncomeTypes { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
    }
}
