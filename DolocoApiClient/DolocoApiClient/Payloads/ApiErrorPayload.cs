using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DolocoApiClient.Payloads
{
    public class ApiErrorPayload
    {
        [JsonProperty("error")]
        public string Message { get; set; }
        public Uri RequestUri { get; set; }
        public HttpMethod RequestMethod { get; set; }
        public HttpStatusCode ResponseStatus { get; set; }
    }
}
