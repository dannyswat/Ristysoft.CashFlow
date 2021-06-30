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
using Ristysoft.CashFlow.ViewModels;

namespace Ristysoft.CashFlow.Pages.Revenues
{
    public class IndexModel : PageModel
    {
        private readonly RevenueService RevenueService;

        public IndexModel(RevenueService revenueService)
        {
            RevenueService = revenueService;
        }

        public PagerViewModel<Revenue> ItemList { get; set; } = new ();

        public async Task OnGetAsync([FromQuery] ListItemsModel listModel)
        {
            ItemList.CurrentPage = listModel.Page;
            ItemList.PageSize = listModel.PageSize;
            ItemList.Items = await RevenueService.ListAsync(listModel);
            ItemList.TotalRecordsCount = await RevenueService.CountAsync(listModel?.Filters);
        }
    }
}
