using System;
using System.Collections.Generic;

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
		public IList<Donation> Donations { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
	}
}

