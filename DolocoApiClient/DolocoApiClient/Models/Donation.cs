using System;
using Newtonsoft.Json;

namespace DolocoApiClient.Models
{
	public class Donation : IDolocoEntity
	{
		public int Id {get; set; }
        [JsonProperty("user_id")]
		public int UserId { get; set; }
		public int BankAccountId { get; set; }
		public int CampaignId { get; set; }
        [JsonProperty("amount")]
		public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
	}
}

