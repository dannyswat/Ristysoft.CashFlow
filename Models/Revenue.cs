using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ristysoft.CashFlow.Models
{
	public class Revenue
	{
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Revenue type")]
        public int RevenueTypeId { get; set; }

        [Display(Name = "Revenue type")]
        public RevenueType RevenueType { get; set; }

        [Display(Name = "Received by")]
        public int? ReceivedByFundId { get; set; }

        [Display(Name = "Received by")]
        public Fund ReceivedBy { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Display(Name = "Remark")]
        [StringLength(300)]
        public string Remark { get; set; }
    }
}
