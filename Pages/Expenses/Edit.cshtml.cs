using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ristysoft.CashFlow.Data;
using Ristysoft.CashFlow.Models;
using Ristysoft.CashFlow.Services;

namespace Ristysoft.CashFlow.Pages.Expenses
{
    public class EditModel : PageModel
    {
        private readonly ExpenseService ExpenseService;

        public EditModel(ExpenseService expenseService)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await ExpenseService.UpdateAsync(Expense);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ExpenseService.ExpenseExists(Expense.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
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
