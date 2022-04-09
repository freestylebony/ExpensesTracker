using ExpensesTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using ExpensesTracker.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ExpensesTracker.Controllers
{
    public class ExpenseController : Controller
    {
       // ExpensesData objexpense = new ExpensesData();
        private readonly IExpenseService expenseService;

        public ExpenseController(IExpenseService _expenseService)
        {
            expenseService = _expenseService;
        }
        public IActionResult Index(string searchString)
        {
            List<Expense> lstEmployee = new List<Expense>();
            lstEmployee = expenseService.GetAllExpenses().ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                lstEmployee = expenseService.GetSearchResult(searchString).ToList();
            }
            return View(lstEmployee);
        }

        public ActionResult AddEditExpenses(int itemId)
        {
            Expense model = new Expense();
            if (itemId > 0)
            {
                model = expenseService.GetExpenseData(itemId);
            }
            return PartialView("_expenseForm", model);
        }

        [HttpPost]
        public ActionResult Create(Expense newExpense)
        {
            if (ModelState.IsValid)
            {
                if (newExpense.ItemId > 0)
                {
                    expenseService.UpdateExpense(newExpense);
                }
                else
                {
                    expenseService.AddExpense(newExpense);
                }
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            expenseService.DeleteExpense(id);
            return RedirectToAction("Index");
        }

        public ActionResult ExpenseSummary()
        {
            return PartialView("_expenseReport");
        }

        public JsonResult GetMonthlyExpense()
        {
            Dictionary<string, decimal> monthlyExpense = expenseService.CalculateMonthlyExpense();
            return new JsonResult(monthlyExpense);
        }

        public JsonResult GetWeeklyExpense()
        {
            Dictionary<string, decimal> weeklyExpense = expenseService.CalculateWeeklyExpense();
            return new JsonResult(weeklyExpense);
        }
    }
}
