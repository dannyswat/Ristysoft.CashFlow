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
    public class DetailsModel : PageModel
    {
        private readonly RevenueService RevenueService;

        public DetailsModel(RevenueService revenueService)
        {
            RevenueService = revenueService;
        }

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
    }
}
