using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ristysoft.CashFlow.ViewModels
{
    public class ListItemsFilter
    {
        public string Property { get; set; }

        public string Operator { get; set; } = "=";

        public object Value { get; set; }
    }
}
