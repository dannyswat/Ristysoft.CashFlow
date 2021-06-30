﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ristysoft.CashFlow.Data;
using Ristysoft.CashFlow.Models;

namespace Ristysoft.CashFlow.Pages.ExpenseTypes
{
    public class IndexModel : PageModel
    {
        private readonly Ristysoft.CashFlow.Data.ApplicationDbContext _context;

        public IndexModel(Ristysoft.CashFlow.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ExpenseType> ExpenseType { get;set; }

        public async Task OnGetAsync()
        {
            ExpenseType = await _context.ExpenseTypes.OrderBy(r => r.Name).ToListAsync();
        }
    }
}
