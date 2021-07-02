using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ristysoft.CashFlow.Models
{
	public class FundTransfer
	{
		[Key]
		public int Id { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime Date { get; set; }

		[Display(Name = "From")]
		public int? FromFundId { get; set; }

		[Display(Name = "From")]
		public Fund From { get; set; }

		[Display(Name = "Paid by")]
		public int? ToFundId { get; set; }

		[Display(Name = "Paid by")]
		public Fund To { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Amount { get; set; }
	}
}
