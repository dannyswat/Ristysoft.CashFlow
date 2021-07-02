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

namespace Ristysoft.CashFlow.Pages.FundTransfers
{
    public class IndexModel : PageModel
    {
        private readonly FundTransferService FundTransferService;

        public IndexModel(FundTransferService fundTransferService)
        {
            FundTransferService = fundTransferService;
        }


        public PagerViewModel<FundTransfer> ItemList { get; set; } = new();

        public async Task OnGetAsync([FromQuery] ListItemsModel listModel)
        {
            ItemList.CurrentPage = listModel.Page;
            ItemList.PageSize = listModel.PageSize;
            ItemList.Items = await FundTransferService.ListAsync(listModel);
            ItemList.TotalRecordsCount = await FundTransferService.CountAsync(listModel?.Filters);
        }
    }
}
