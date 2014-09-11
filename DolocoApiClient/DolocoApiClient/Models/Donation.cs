using System;
using Newtonsoft.Json;

namespace DolocoApiClient.Models
{
	public class Donation : IDolocoEntity
	{
		public int Id {get; set; }
		public int UserId { get; set; }
		public int BankAccountId { get; set; }
		public int CampaignId { get; set; }
		public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
	}
}

