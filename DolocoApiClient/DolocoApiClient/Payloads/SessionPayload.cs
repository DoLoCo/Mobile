using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DolocoApiClient.Models;
using Newtonsoft.Json;

namespace DolocoApiClient.Payloads
{
    internal class SessionPayload
    {
        [JsonProperty("user")]
        public string User { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
