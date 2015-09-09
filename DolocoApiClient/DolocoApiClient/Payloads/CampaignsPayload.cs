﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DolocoApiClient.Models;
using Newtonsoft.Json;

namespace DolocoApiClient.Payloads
{
    internal class CampaignsPayload
    {
        [JsonProperty("campaigns")]
        public IEnumerable<Campaign> Campaigns { get; set; }
    }
}
