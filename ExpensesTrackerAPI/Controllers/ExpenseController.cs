using ExpensesTracker.Interfaces;
using ExpensesTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesTrackerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService expenseService;

        public ExpenseController(IExpenseService _expenseService)
        {
            expenseService = _expenseService;
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
    }
}
