using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ristysoft.CashFlow.Data;
using Ristysoft.CashFlow.Models;

namespace Ristysoft.CashFlow.Pages.RevenueTypes
{
    public class CreateModel : PageModel
    {
        private readonly Ristysoft.CashFlow.Data.ApplicationDbContext _context;

        public CreateModel(Ristysoft.CashFlow.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RevenueType RevenueType { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.RevenueTypes.Add(RevenueType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
