using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuotaInvoice.Shared.Entities
{
	public class MetroCard
	{
		public Guid Id { get; set; }
		[Required, StringLength(10)]
		public string Number { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }
		[Required]
		public string UserId { get; set; }
	}
}

