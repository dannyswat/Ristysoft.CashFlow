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
    public class DeleteModel : PageModel
    {
        private readonly ExpenseService ExpenseService;

        public DeleteModel(ExpenseService expenseService)
        {
            ExpenseService = expenseService;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                await ExpenseService.DeleteAsync(id.Value);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
