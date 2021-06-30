using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ristysoft.CashFlow.Data;
using Ristysoft.CashFlow.Models;
using Ristysoft.CashFlow.Services;

namespace Ristysoft.CashFlow.Pages.Expenses
{
    public class DetailsModel : PageModel
    {
        private readonly ExpenseService ExpenseService;

        public DetailsModel(ExpenseService expenseService)
        {
            ExpenseService = expenseService;
        }

        public Expense Expense { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Expense = await ExpenseService.GetAsync(id.Value);

            if (Expense == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
