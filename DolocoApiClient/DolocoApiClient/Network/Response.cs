using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace DolocoApiClient.Network
{
    public class Response<TPayload>
    {
        private Payloads.ApiErrorPayload errorPayload;

        public HttpStatusCode Status { get; set; }
        public TPayload Payload { get; set; }
        public KeyValuePair<string, IList<string>> Errors { get; set; }

        public Response(TPayload payload)
        {
            Payload = payload;
        }

        public Response(Payloads.ApiErrorPayload errorPayload)
        {
            // TODO: Complete member initialization
            this.errorPayload = errorPayload;
        }
    }
}
