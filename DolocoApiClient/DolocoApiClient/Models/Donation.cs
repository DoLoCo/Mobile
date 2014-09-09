using System;

namespace DolocoApiClient.Models
{
	public class Donation : IDolocoEntity
	{
		public int Id {get; set; }
		public int UserId { get; set; }
		public int BankAccountId { get; set; }
		public int CampaignId { get; set; }
		public float Ammount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
	}
}

