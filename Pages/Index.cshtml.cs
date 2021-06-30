using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ristysoft.CashFlow.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly Ristysoft.CashFlow.Data.ApplicationDbContext _context;

		public IndexModel(ILogger<IndexModel> logger,
			Ristysoft.CashFlow.Data.ApplicationDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		public async Task OnGet()
		{
			var date = DateTime.Today;
			var monthStart = new DateTime(date.Year, date.Month, 1);
			ViewData["ThisMonthExpense"] = (await _context.Expenses.Where(r =>
				r.Date >= monthStart &&
				r.Date < monthStart.AddMonths(1))
				.Select(r => r.Amount).ToArrayAsync())
				.Sum();

			ViewData["LastMonthExpense"] = (await _context.Expenses.Where(r =>
				r.Date >= monthStart.AddMonths(-1) &&
				r.Date < monthStart)
				.Select(r => r.Amount).ToArrayAsync())
				.Sum();
		}
	}
}
