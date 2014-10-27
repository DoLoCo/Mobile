using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DolocoApiClient.Models
{
    public enum CampaignStatus
    {
        Active,
        Complete
    }

	public class Campaign : IDolocoEntity
	{
		public int Id {get; set; }
		public int OrganizationId { get; set; }
		public int BankAccountId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public CampaignStatus Status { get; set; }
        [JsonProperty("organization")]
        public Organization Organization { get; set; }
        [JsonProperty("donations_amount_sum")]
		public int DonationAmount { get; set; }
        
        [JsonProperty("target_amount")]
        public int? TargetAmount { get; set; }
        [JsonProperty("target_date")]
        public DateTime? TargetDate { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
	}
}

