using ExpensesTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Data
{
    public class ExpenseDBContext : DbContext
    {
            public ExpenseDBContext(DbContextOptions<ExpenseDBContext> options)
                : base(options)
            {
            }

            public DbSet<ExpensesTracker.Models.Expense> Expenses { get; set; }

    }

    
}
