using Microsoft.AspNetCore.Mvc.Rendering;
using Ristysoft.CashFlow.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ristysoft.CashFlow
{
    public class SelectLists
    {
        readonly ApplicationDbContext _context;
        public SelectLists(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public SelectList FundList() => new SelectList(
                _context.Funds
                    .Where(r => r.Disabled == false)
                    .OrderBy(r => r.Name)
                    .Select(r => new SelectListItem(r.Name, r.Id.ToString())
                ), "Value", "Text");


        public SelectList ExpenseTypeList() => new SelectList(
                _context.ExpenseTypes
                    .Where(r => r.Disabled == false)
                    .OrderBy(r => r.Name)
                    .Select(r => new SelectListItem(r.Name, r.Id.ToString())), "Value", "Text");

        public SelectList RevenueTypeList() => new SelectList(
                _context.RevenueTypes
                    .Where(r => r.Disabled == false)
                    .OrderBy(r => r.Name)
                    .Select(r => new SelectListItem(r.Name, r.Id.ToString())), "Value", "Text");
    }

    public static class NullableSelectList
    {
        static readonly SelectListItem EmptyItem = new SelectListItem("", "");
        static readonly List<SelectListItem> NullableList = new List<SelectListItem>
        { EmptyItem };

        public static SelectList Nullable(this IEnumerable<SelectListItem> items)
        {
            return new SelectList(NullableList.Concat(items), "Value", "Text");
        }
    }
}
