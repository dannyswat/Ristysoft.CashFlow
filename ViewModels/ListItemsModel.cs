using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ristysoft.CashFlow.ViewModels
{
    public class ListItemsModel
    {
        int page = 1;

        public int Page { get => page; set => page = value < 1 ? 1 : value; }


        public string Sort { get; set; }
        
        [ModelBinder(BinderType = typeof(ModelBinders.JsonModelBinder))]
        public List<ListItemsFilter> Filters { get; set; }

        public int PageSize { get; set; } = 50;

        public int SkipRecordsCount
        {
            get
            {
                return (Page - 1) * PageSize;
            }
        }
    }
}
