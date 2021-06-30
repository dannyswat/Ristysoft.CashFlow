using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ristysoft.CashFlow.Pages.Funds
{
	public class TransferModel : PageModel
	{
		private readonly Ristysoft.CashFlow.Data.ApplicationDbContext _context;

		public TransferModel(Ristysoft.CashFlow.Data.ApplicationDbContext context)
		{
			_context = context;
		}

		[BindProperty]
		public TransferDataModel Transfer { get; set; }

		public IActionResult OnGet()
		{
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			var fromFund = await _context.Funds.FindAsync(Transfer.FromFundId);
			var toFund = await _context.Funds.FindAsync(Transfer.ToFundId);
			if (fromFund == null || toFund == null) return NotFound();

			fromFund.Balance -= Transfer.Amount;
			toFund.Balance += Transfer.Amount;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
				return Page();
			}

			return RedirectToPage("./Index");
		}

		public class TransferDataModel
		{
			[Display(Name = "From Fund")]
			public int FromFundId { get; set; }

			[Display(Name = "To Fund")]
			public int ToFundId { get; set; }

			public decimal Amount { get; set; }
		}
	}
}
