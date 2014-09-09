using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using DolocoApiClient.Models;

namespace DolocoApiClient
{
	public interface IDolocoApiClient
	{
        string TestCreateSession();
        Task<string> RegisterUserAsync(string email, string firsName, string lastName, string password,
            string passwordConfirmation);
	    Task<string> CreateSessionAsync(string email, string password);
		Task<IEnumerable<Organization>> GetMyOrganizationsAsync ();
        Task<Organization> GetOrganizationAsync(int organizationId);
	    Task<Organization> CreateOrganizationAsync(string name, string phoneNumber, string description, string address,
	        string city, string state, string postalCode);
	    Task<BankAccount> CreateBankAccountAsync(string accountNumber, string accountType, string routingNumber,
	        string accountName);
        Task<IEnumerable<Campaign>> GetOrganizationCampaignAsync(int organizationId);
	}
}

