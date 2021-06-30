using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ristysoft.CashFlow.Models
{
    public class ExpenseType
    { 
        [Key]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Name { get; set; }

        public bool Disabled { get; set; }
    }
}
