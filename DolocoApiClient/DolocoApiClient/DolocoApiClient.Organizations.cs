﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DolocoApiClient.Models;
using DolocoApiClient.Network;
using DolocoApiClient.Payloads;

namespace DolocoApiClient
{
    public partial class DolocoApiClient
    {
        public Task<IEnumerable<Organization>> GetMyOrganizationsAsync()
        {
            var myOrganizaitonsUrl = GetRoutePathUrl(DolocoApiRouteEnum.MyOrganizations);

            return _client.GetAsync<OrganizationsPayload>(myOrganizaitonsUrl).Process(payload =>
            {
                var organizations = new List<Organization>();

                if (payload.Organizations != null)
                    organizations = payload.Organizations.ToList();

                return organizations.AsEnumerable();
            });
        }

        public Task<IEnumerable<Organization>> GetOrganizationsAsync()
        {
            var organizaitonsUrl = GetRoutePathUrl(DolocoApiRouteEnum.Organizations);

            return _client.GetAsync<OrganizationsPayload>(organizaitonsUrl).Process(payload =>
            {
                var organizations = payload.Organizations.ToList();

                return organizations.AsEnumerable();
            });
        }

        public Task<Organization> GetOrganizationAsync(int organizationId)
        {
            var organizationUrl = String.Format(GetRoutePathUrl(DolocoApiRouteEnum.Organization), organizationId);

            return _client.GetAsync<Organization>(organizationUrl).Process();
        }

        public Task<Organization> CreateOrganizationAsync(string name, string phoneNumber, string description, string address, string city, string state, string postalCode)
        {
            var organizationUrl = GetRoutePathUrl(DolocoApiRouteEnum.Organizations);
            var organizationPayload = new Dictionary<string, string>
            {
                {"name", name},
                {"phone_number", phoneNumber},
                {"description", description},
                {"address_line1", address},
                {"city", city},
                {"state", state},
                {"postal_code", postalCode}
            };

            return _client.PostAsync<Organization>(organizationUrl, organizationPayload).Process();
        }

        public Task<Organization> UpdateOrganizationAsync(int organizationId, string name, string phoneNumber, string description,
            string address, string city, string state, string postalCode)
        {
            var organizationUrl = String.Format(GetRoutePathUrl(DolocoApiRouteEnum.Organization), organizationId);
            var organizationPayload = new Dictionary<string, string>
            {
                {"name", name},
                {"phone_number", phoneNumber},
                {"description", description},
                {"address_line_1", address},
                {"city", city},
                {"state", state},
                {"postal_code", postalCode}
            };

            return _client.PutAsync<Organization>(organizationUrl, organizationPayload).Process();
        }

        public Task<Organization> DeleteOrganizationAsync(int organizationId)
        {
            var organizationUrl = String.Format(GetRoutePathUrl(DolocoApiRouteEnum.Organization), organizationId);

            return _client.DeleteAsync<Organization>(organizationUrl).Process();
        }

        public Task<IEnumerable<BankAccount>> GetOrganizationBankAccountsAsync(int organizationId)
        {
            var organizationUrl = String.Format(GetRoutePathUrl(DolocoApiRouteEnum.OrganizationBankAccounts), organizationId);

            return _client.GetAsync<BankAccountsPayload>(organizationUrl).Process(payload =>
            {
                var bankAccounts = payload.BankAccounts.ToList();

                return bankAccounts.AsEnumerable();
            });
        }

        public Task<BankAccount> GetOrganizationBankAccountAsync(int organizationId, int bankAccountId)
        {
            var bankAccountUrl = String.Format(GetRoutePathUrl(DolocoApiRouteEnum.OrganizationBankAccount),
                organizationId, bankAccountId);

            return _client.GetAsync<BankAccount>(bankAccountUrl).Process();
        }

        public Task<BankAccount> CreateOrganizationBankAccountAsync(int organizationId, string accountNumber,
            string accountType, string routingNumber,
            string accountName)
        {
            var bankAccountUrl = String.Format(GetRoutePathUrl(DolocoApiRouteEnum.OrganizationBankAccounts),
                organizationId);

            var bankAccount = new Dictionary<string, string>
            {
                {"account_number", accountNumber},
                {"account_type", accountType},
                {"bank_account_name", accountName},
                {"routing_number", routingNumber}
            };

            var bankAccountPayload = new Dictionary<string, Dictionary<string, string>>
            {
                {"bank_account", bankAccount}
            };

            return _client.PostAsync<BankAccount>(bankAccountUrl, bankAccountPayload).Process();
        }

        public Task<IEnumerable<Campaign>> GetOrganizationCampaignAsync(int organizationId)
        {
            var campaignsUrl = GetRoutePathUrl(DolocoApiRouteEnum.Campaigns);
            var organizationCampaignsUrl = String.Format("{0}?organization_id={1}", campaignsUrl, organizationId);

            return _client.GetAsync<CampaignsPayload>(organizationCampaignsUrl).Process(payload =>
            {
                var campaigns = new List<Campaign>();

                if (payload.Campaigns != null)
                    campaigns = payload.Campaigns.ToList();

                return campaigns.AsEnumerable();
            });
        }

        public Task<Campaign> CreateOrganizationCampaignAsync(int organizationId, string campaignName,
            string campaignDescription, string campaignTarget, DateTime campaignTargetDate)
        {
            var createCampaignUrl = String.Format(GetRoutePathUrl(DolocoApiRouteEnum.OrganizationCampaigns),
                organizationId);



            var campaign = new Dictionary<string, string>
            {
                {"title", campaignName},
                {"description", campaignDescription},
                {"target_amount", ConvertToCents(campaignTarget)},
                {"target_date", campaignTargetDate.ToString("yyyy-mm-dd")}
            };

            var campaignPayload = new Dictionary<string, Dictionary<string, string>>
            {
                {"campaign", campaign}
            };

            return _client.PostAsync<Campaign>(createCampaignUrl, campaignPayload).Process(payload =>
            {
                var newCampaign = (Campaign) payload;

                return newCampaign;
            });
        }

        public Task<IEnumerable<Donation>> GetOrganizationCampaignDonationsAsync(int organizationId, int campaignId)
        {
            var donationUrl = String.Format(GetRoutePathUrl(DolocoApiRouteEnum.OrganizationCampaignDonations),
                organizationId, campaignId);

            return _client.GetAsync<DonationsPayload>(donationUrl).Process(payload =>
            {
                var donations = new List<Donation>();

                if (payload.Donations != null)
                {
                    donations = payload.Donations.ToList();
                }

                return donations.AsEnumerable();
            });
        }

        public Task<Donation> CreateOrganizationCampaignDonationAsync(int organizationId, int campaignId, string amount,
            int bankAccountId)
        {
            var donationUrl = String.Format(GetRoutePathUrl(DolocoApiRouteEnum.OrganizationCampaignDonations),
                organizationId, campaignId);

            var donation = new Dictionary<string, int>
            {
                {"amount", int.Parse(ConvertToCents(amount))},
                {"bank_account_id", bankAccountId}
            };

            var donationPayload = new Dictionary<string, Dictionary<string, int>>
            {
                {"donation", donation}
            };

            return _client.PostAsync<Donation>(donationUrl, donationPayload).Process();
        }
    }
}
