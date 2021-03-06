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

        public Task<IEnumerable<Campaign>> GetMyCampaignsAsync()
        {
            var campaignUrl = GetRoutePathUrl(DolocoApiRouteEnum.Campaigns);
            return _client.GetAsync<CampaignsPayload>(campaignUrl).Process(payload =>
            {
                var campaigns = new List<Campaign>();

                if (payload.Campaigns != null)
                    campaigns = payload.Campaigns.ToList();

                return campaigns.AsEnumerable();
            });
        }

        public Task<Campaign> GetCampaignAsync(int campaignId)
        {
            var campaignUrl = GetRoutePathUrl(DolocoApiRouteEnum.Campaign);

            return _client.GetAsync<Campaign>(String.Format(campaignUrl, campaignId)).Process();
        }

        public Task<IEnumerable<Campaign>> GetNearbyCampaignsAsync(double lat, double lng)
        {
            var campaignsUrl = String.Format("{0}?latitude={1}&longitude={2}", GetRoutePathUrl(DolocoApiRouteEnum.Campaigns), lat,
                lng);

            return _client.GetAsync<CampaignsPayload>(campaignsUrl).Process(payload =>
            {
                var campaigns = new List<Campaign>();

                if (payload.Campaigns != null)
                    campaigns = payload.Campaigns.ToList();

                return campaigns.AsEnumerable();
            });
        } 
    }
}
