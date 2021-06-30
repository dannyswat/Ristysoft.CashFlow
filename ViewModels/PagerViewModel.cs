using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ristysoft.CashFlow.ViewModels
{
    public class PagerViewModel<T> : PagerViewModel
    {
        public IList<T> Items { get; set; }
    }

    public class PagerViewModel
    {
        int? pageCount;
        int pageSize = 50;
        int totalCount;
        int page = 1;

        public int CurrentPage { get => page; set => page = value < 1 ? 1 : value; }

        public int TotalRecordsCount { get => totalCount; set { totalCount = value; pageCount = null; } }

        public int PageSize { get => pageSize; set { pageSize = value; pageCount = null; } }

        public int PagesCount
        {
            get
            {
                if (pageCount == null)
                    pageCount = TotalRecordsCount / PageSize
                           + (TotalRecordsCount % PageSize > 0 ? 1 : 0);
                return pageCount.Value;
            }
        }
    }
}
