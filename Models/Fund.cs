using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ristysoft.CashFlow.Models
{
    public class Fund
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        public bool Disabled { get; set; }
    }
}
