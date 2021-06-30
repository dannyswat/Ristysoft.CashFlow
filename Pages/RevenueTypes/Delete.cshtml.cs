using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ristysoft.CashFlow.Data;
using Ristysoft.CashFlow.Models;

namespace Ristysoft.CashFlow.Pages.RevenueTypes
{
    public class DeleteModel : PageModel
    {
        private readonly Ristysoft.CashFlow.Data.ApplicationDbContext _context;

        public DeleteModel(Ristysoft.CashFlow.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RevenueType RevenueType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RevenueType = await _context.RevenueTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (RevenueType == null)
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

            RevenueType = await _context.RevenueTypes.FindAsync(id);

            if (RevenueType != null)
            {
                _context.RevenueTypes.Remove(RevenueType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
