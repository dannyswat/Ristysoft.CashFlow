using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ristysoft.CashFlow.Data;
using Ristysoft.CashFlow.Models;
using Ristysoft.CashFlow.Services;

namespace Ristysoft.CashFlow.Pages.Revenues
{
    public class CreateModel : PageModel
    {
        private readonly RevenueService RevenueService;

        public CreateModel(RevenueService revenueService)
        {
            RevenueService = revenueService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Revenue Revenue { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await RevenueService.CreateAsync(Revenue);
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
