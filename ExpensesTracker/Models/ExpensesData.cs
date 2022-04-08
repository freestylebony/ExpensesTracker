using ExpensesTracker.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesTracker.Models
{
    public class ExpensesData
    {
        ExpenseDBContext db = new ExpenseDBContext();
        public IEnumerable<Expense> GetAllExpenses()
        {
            try
            {
                return db.Expenses.ToList();
            }
            catch
            {
                throw;
            }
        }

        // To filter out the records based on the search string   
        public IEnumerable<Expense> GetSearchResult(string searchString)
        {
            List<Expense> exp = new List<Expense>();
            try
            {
                exp = GetAllExpenses().ToList();
                return exp.Where(x => x.ItemName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1);
            }
            catch
            {
                throw;
            }
        }

        //To Add new Expense record         
        public void AddExpense(Expense expense)
        {
            try
            {
                db.Expenses.Add(expense);
                db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //To Update the records of a particluar expense    
        public int UpdateExpense(Expense expense)
        {
            try
            {
                db.Entry(expense).State = EntityState.Modified;
                db.SaveChanges();

                return 1;
            }
            catch
            {
                throw;
            }
        }

        //Get the data for a particular expense    
        public Expense GetExpenseData(int id)
        {
            try
            {
                Expense expense = db.Expenses.Find(id);
                return expense;
            }
            catch
            {
                throw;
            }
        }

        //To Delete the record of a particular expense    
        public void DeleteExpense(int id)
        {
            try
            {
                Expense emp = db.Expenses.Find(id);
                db.Expenses.Remove(emp);
                db.SaveChanges();

            }
            catch
            {
                throw;
            }
        }

        // To calculate last six months expense  
        public Dictionary<string, decimal> CalculateMonthlyExpense()
        {
            ExpensesData objexpense = new ExpensesData();
            List<Expense> lstEmployee = new List<Expense>();

            Dictionary<string, decimal> dictMonthlySum = new Dictionary<string, decimal>();

            decimal foodSum = db.Expenses.Where
                (cat => cat.Category == "Food" && (cat.ExpenseDate > DateTime.Now.AddMonths(-7)))
                .Select(cat => cat.Amount)
                .Sum();

            decimal shoppingSum = db.Expenses.Where
               (cat => cat.Category == "Shopping" && (cat.ExpenseDate > DateTime.Now.AddMonths(-7)))
               .Select(cat => cat.Amount)
               .Sum();

            decimal travelSum = db.Expenses.Where
               (cat => cat.Category == "Travel" && (cat.ExpenseDate > DateTime.Now.AddMonths(-7)))
               .Select(cat => cat.Amount)
               .Sum();

            decimal healthSum = db.Expenses.Where
               (cat => cat.Category == "Health" && (cat.ExpenseDate > DateTime.Now.AddMonths(-7)))
               .Select(cat => cat.Amount)
               .Sum();

            dictMonthlySum.Add("Food", foodSum);
            dictMonthlySum.Add("Shopping", shoppingSum);
            dictMonthlySum.Add("Travel", travelSum);
            dictMonthlySum.Add("Health", healthSum);

            return dictMonthlySum;
        }

        // To calculate last four weeks expense  
        public Dictionary<string, decimal> CalculateWeeklyExpense()
        {
            ExpensesData objexpense = new ExpensesData();
            List<Expense> lstEmployee = new List<Expense>();

            Dictionary<string, decimal> dictWeeklySum = new Dictionary<string, decimal>();

            decimal foodSum = db.Expenses.Where
                (cat => cat.Category == "Food" && (cat.ExpenseDate > DateTime.Now.AddDays(-7)))
                .Select(cat => cat.Amount)
                .Sum();

            decimal shoppingSum = db.Expenses.Where
               (cat => cat.Category == "Shopping" && (cat.ExpenseDate > DateTime.Now.AddDays(-28)))
               .Select(cat => cat.Amount)
               .Sum();

            decimal travelSum = db.Expenses.Where
               (cat => cat.Category == "Travel" && (cat.ExpenseDate > DateTime.Now.AddDays(-28)))
               .Select(cat => cat.Amount)
               .Sum();

            decimal healthSum = db.Expenses.Where
               (cat => cat.Category == "Health" && (cat.ExpenseDate > DateTime.Now.AddDays(-28)))
               .Select(cat => cat.Amount)
               .Sum();

            dictWeeklySum.Add("Food", foodSum);
            dictWeeklySum.Add("Shopping", shoppingSum);
            dictWeeklySum.Add("Travel", travelSum);
            dictWeeklySum.Add("Health", healthSum);

            return dictWeeklySum;
        }
    }
}
