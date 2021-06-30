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

namespace Ristysoft.CashFlow.Pages.Revenues
{
    public class DeleteModel : PageModel
    {
        private readonly RevenueService RevenueService;

        public DeleteModel(RevenueService revenueService)
        {
            RevenueService = revenueService;
        }

        [BindProperty]
        public Revenue Revenue { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Revenue = await RevenueService.GetAsync(id.Value);

            if (Revenue == null)
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
                await RevenueService.DeleteAsync(id.Value);
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
