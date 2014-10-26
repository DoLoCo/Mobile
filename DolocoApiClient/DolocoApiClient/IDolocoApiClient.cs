using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using DolocoApiClient.Models;
using DolocoApiClient.Payloads;

namespace DolocoApiClient
{
	public interface IDolocoApiClient
	{
        string TestCreateSession();
        Task<string> RegisterUserAsync(string email, string firsName, string lastName, string password,
            string passwordConfirmation);
        Task<Dictionary<string, object>> CreateSessionAsync(string email, string password);
		Task<IEnumerable<Organization>> GetMyOrganizationsAsync ();
        Task<Organization> GetOrganizationAsync(int organizationId);
	    Task<Organization> CreateOrganizationAsync(string name, string phoneNumber, string description, string address,
	        string city, string state, string postalCode);

	    Task<BankAccount> CreateOrganizationBankAccountAsync(int organizationId, string accountNumber,
	        string accountType, string routingNumber,
	        string accountName);
	    Task<BankAccount> CreateBankAccountAsync(string accountNumber, string accountType, string routingNumber,
	        string accountName);

	    Task<IEnumerable<BankAccount>> GetBankAccountsAsync();
        Task<IEnumerable<Campaign>> GetOrganizationCampaignAsync(int organizationId);

	    Task<Campaign> CreateOrganizationCampaignAsync(int organizationId, string campaignName,
	        string campaignDescription, string campaignTarget, DateTime campaignTargetDate);

	    Task<Campaign> GetCampaignAsync(int campaignId);
	    Task<IEnumerable<Campaign>> GetNearbyCampaignsAsync(double lat, double lng);
	    Task<IEnumerable<Donation>> GetOrganizationCampaignDonationsAsync(int organizationId, int campaignId);

	    Task<Donation> CreateOrganizationCampaignDonationAsync(int organizationId, int campaignId, string amount,
	        int bankAccountId);

	    Task VerifyBankAccountAsync(int bankAccountId, decimal amount1, decimal amount2);
	}
}

