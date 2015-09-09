using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace DolocoApiClient.Network
{
    public static class HttpWebResponseExtensions
    {
        public static TResponseBody GetResponseBody<TResponseBody>(this HttpWebResponse response)
        {
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<TResponseBody>(reader.ReadToEnd());
            }
        }
    }
}
