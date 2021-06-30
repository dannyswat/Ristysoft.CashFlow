using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ristysoft.CashFlow.Data;
using Ristysoft.CashFlow.Models;

namespace Ristysoft.CashFlow.Pages.Funds
{
    public class DeleteModel : PageModel
    {
        private readonly Ristysoft.CashFlow.Data.ApplicationDbContext _context;

        public DeleteModel(Ristysoft.CashFlow.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Fund Fund { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Fund = await _context.Funds.FirstOrDefaultAsync(m => m.Id == id);

            if (Fund == null)
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

            Fund = await _context.Funds.FindAsync(id);

            if (Fund != null)
            {
                _context.Funds.Remove(Fund);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
