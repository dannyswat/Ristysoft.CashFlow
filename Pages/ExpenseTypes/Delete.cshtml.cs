using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ristysoft.CashFlow.Data;
using Ristysoft.CashFlow.Models;

namespace Ristysoft.CashFlow.Pages.ExpenseTypes
{
    public class DeleteModel : PageModel
    {
        private readonly Ristysoft.CashFlow.Data.ApplicationDbContext _context;

        public DeleteModel(Ristysoft.CashFlow.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ExpenseType ExpenseType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ExpenseType = await _context.ExpenseTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (ExpenseType == null)
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

            ExpenseType = await _context.ExpenseTypes.FindAsync(id);

            if (ExpenseType != null)
            {
                _context.ExpenseTypes.Remove(ExpenseType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
