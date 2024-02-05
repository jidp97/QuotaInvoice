using System;
using System.ComponentModel.DataAnnotations;

namespace QuotaInvoice.Shared.Entities
{
	public class CreditCard
	{
		public Guid Id { get; set; }
		[Required, StringLength(16)]
		public string Number { get; set; }
		[Required, StringLength(5) ]
		public string Date { get; set; }
		[Required, StringLength(3)]
		public string CVV { get; set; }
		public string UserId { get; set; }
	}
}

