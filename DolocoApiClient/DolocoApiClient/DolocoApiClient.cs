using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DolocoApiClient.Network;


namespace DolocoApiClient
{
	public partial class DolocoApiClient : IDolocoApiClient
    {
        private readonly IRestClient _client;
        private IDictionary<DolocoApiRouteEnum, string> _routes;
        private string _baseUri;
		public string Token;

        public DolocoApiClient(string baseUri)
        {
			Token = null;
            _baseUri = baseUri;

            InitRoutes();

            _client = new JsonRestClient(baseUri);
        }

        public void InitRoutes()
        {
            _routes = new Dictionary<DolocoApiRouteEnum, string>
            {
                {DolocoApiRouteEnum.SessionTest, "/session/test"},
                {DolocoApiRouteEnum.Session, "/session"},
                {DolocoApiRouteEnum.Registration, "/registration"},
                {DolocoApiRouteEnum.BankAccounts, "/bank_accounts"},
                {DolocoApiRouteEnum.BankAccount, "/bank_accounts/{0}"},
                {DolocoApiRouteEnum.Organizations, "/organizations"},
                {DolocoApiRouteEnum.Organization, "/organizations/{0}"},
                {DolocoApiRouteEnum.MyOrganizations, "/organizations/mine"},
                {DolocoApiRouteEnum.OrganizationBankAccounts, "/organizations/{0}/bank_accounts"},
                {DolocoApiRouteEnum.OrganizationBankAccount, "/organizations/{0}/bank_accounts/{1}"},
                {DolocoApiRouteEnum.OrganizationCampaigns, "/organizations/{0}/campaigns"},
                {DolocoApiRouteEnum.OrganizationCampaign, "/organizations/{0}/campaigns/{1}"},
                {DolocoApiRouteEnum.OrganizationCampaignDonations, "/organizations/{0}/campaigns/{1}/donations"},
                {DolocoApiRouteEnum.Campaigns, "/campaigns"},
                {DolocoApiRouteEnum.Campaign, "/campaigns/{0}"}
            };
        }

	    private string GetRoutePathUrl(DolocoApiRouteEnum route)
	    {
	        string routePath;

	        _routes.TryGetValue(route, out routePath);

	        return routePath;
	    }
    }
}
