using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ristysoft.CashFlow.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Expense type")]
        public int ExpenseTypeId { get; set; }

        [Display(Name = "Expense type")]
        public ExpenseType ExpenseType { get; set; }

        [Display(Name = "Paid by")]
        public int? PaidByFundId { get; set; }

        [Display(Name = "Paid by")]
        public Fund PaidBy { get; set; }


        [Display(Name = "Remark")]
        [StringLength(300)]
        public string Remark { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
    }
}
