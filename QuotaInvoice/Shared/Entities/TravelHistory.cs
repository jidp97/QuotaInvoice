using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuotaInvoice.Shared.Entities
{
	public class TravelHistory
	{
		public Guid Id { get; set; }
		[Required, DataType(DataType.Date)]
		public DateTime EntryDate { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }
		public bool Active { get; set; }
		[DataType(DataType.Currency), Column(TypeName = "decimal(18, 2)")]
		public decimal Cost { get; set; }

		public Guid MetroCardId { get; set; }
		public MetroCard MetroCard { get; set; }
	}
}

