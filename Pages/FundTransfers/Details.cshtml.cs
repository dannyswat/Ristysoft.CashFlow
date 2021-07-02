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

namespace Ristysoft.CashFlow.Pages.FundTransfers
{
    public class DetailsModel : PageModel
    {
        private readonly FundTransferService FundTransferService;

        public DetailsModel(FundTransferService fundTransferService)
        {
            FundTransferService = fundTransferService;
        }

        public FundTransfer FundTransfer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FundTransfer = await FundTransferService.GetAsync(id.Value);

            if (FundTransfer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
