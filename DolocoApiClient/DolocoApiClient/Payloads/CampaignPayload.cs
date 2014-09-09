using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DolocoApiClient.Models;
using Newtonsoft.Json;

namespace DolocoApiClient.Payloads
{
    internal class CampaignPayload
    {
        [JsonProperty("campaign")]
        public Campaign Campaign { get; set; }
    }
}
