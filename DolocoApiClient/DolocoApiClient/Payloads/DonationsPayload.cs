﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DolocoApiClient.Models;
using Newtonsoft.Json;

namespace DolocoApiClient.Payloads
{
    internal class DonationsPayload
    {
        [JsonProperty("donations")]
        public IEnumerable<Donation> Donations { get; set; }
    }
}
