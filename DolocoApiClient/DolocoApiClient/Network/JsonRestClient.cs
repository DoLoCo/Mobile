using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DolocoApiClient.Models;
using DolocoApiClient.Payloads;
using Newtonsoft.Json;

namespace DolocoApiClient.Network
{
    public class JsonRestClient : IRestClient
    {
        private readonly string _baseUri;
        private readonly Func<HttpClient> _httpClientFactory;
		private string _token;

        public JsonRestClient(string baseUri, Func<HttpClient> httpClientFactory = null)
        {
            _baseUri = baseUri;
            _httpClientFactory = httpClientFactory ?? new Func<HttpClient>(() => new HttpClient());
        }

		public void SetToken(string token) {
			_token = token;
		}

        public Task<Response<TResponsePayload>> GetAsync<TResponsePayload>(string url)
        {
            return makeRequestAsync<TResponsePayload>(url, HttpMethod.Get);
        }

        public Task<Response<TResponsePayload>> PostAsync<TResponsePayload>(string url, object payload)
        {
            return makeRequestAsync<TResponsePayload>(url, HttpMethod.Post, payload);
        }

        public Task<Response<TResponsePayload>> DeleteAsync<TResponsePayload>(string url)
        {
            return makeRequestAsync<TResponsePayload>(url, HttpMethod.Delete);
        }

        public Task<Response<TResponsePayload>> PutAsync<TResponsePayload>(string url, object payload)
        {
            return makeRequestAsync<TResponsePayload>(url, HttpMethod.Put, payload);
        }

        private async Task<Response<TResponsePayload>> makeRequestAsync<TResponsePayload>(string url, HttpMethod method,
			object payload = null)
        {
            var uri = new Uri(_baseUri + url);
            Debug.WriteLine(method + ": " + uri);

            var client = _httpClientFactory();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			if (_token != null)
			{
			    var authHeader = String.Format("Token token=\"{0}\"", _token);
				client.DefaultRequestHeaders.Add("Authorization", authHeader);

                Debug.WriteLine(client.DefaultRequestHeaders.ToString());
			}

            if (method == HttpMethod.Post || method == HttpMethod.Put)
            {
                var json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json);
                Debug.WriteLine("Payload: " + json);

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                return await processResponse<TResponsePayload>(await client.PostAsync(uri, content));
            }

            return await processResponse<TResponsePayload>(await client.GetAsync(uri));
        }

        private async Task<Response<TResponsePayload>> processResponse<TResponsePayload>(HttpResponseMessage response)
        {
            var responseBody = await response.Content.ReadAsStringAsync();

            Debug.WriteLine("Response: " + responseBody);

            if (response.IsSuccessStatusCode)
            {
                var payload = JsonConvert.DeserializeObject<TResponsePayload>(responseBody);

                return new Response<TResponsePayload>(payload);
            } else {
                var error = JsonConvert.DeserializeObject<Error>(responseBody);
                Debug.WriteLine(error.Message);
                throw new Exception("Oops! Something Broke :(");
            }

            return null;
        }
    }
}
