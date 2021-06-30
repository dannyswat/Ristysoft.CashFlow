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

namespace Ristysoft.CashFlow.Pages.Revenues
{
    public class EditModel : PageModel
    {
        private readonly RevenueService RevenueService;

        public EditModel(RevenueService revenueService)
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
                await RevenueService.UpdateAsync(Revenue);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RevenueService.RevenueExists(Revenue.Id))
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
