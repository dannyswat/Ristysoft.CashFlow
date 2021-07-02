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

namespace Ristysoft.CashFlow.Pages.FundTransfers
{
    public class CreateModel : PageModel
    {
        private readonly FundTransferService FundTransferService;

        public CreateModel(FundTransferService fundTransferService)
        {
            FundTransferService = fundTransferService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public FundTransfer FundTransfer { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await FundTransferService.CreateAsync(FundTransfer);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return Page();
            }

            return RedirectToPage("/Funds/Index");
        }
    }
}
