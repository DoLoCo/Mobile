using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DolocoApiClient.Models;
using DolocoApiClient.Network;
using DolocoApiClient.Payloads;

namespace DolocoApiClient
{
    public partial class DolocoApiClient
    {
        public string TestCreateSession()
        {
            var sessionTestPath = GetRoutePathUrl(DolocoApiRouteEnum.SessionTest);

            return "hello world";
        }

        public Task<Dictionary<string, object>> CreateSessionAsync(string email, string password)
        {
            var createSessionPath = GetRoutePathUrl(DolocoApiRouteEnum.Session);
            var postPayload = new Dictionary<string, string>
            {
                {"email", email},
                {"password", password}
            };

			return _client.PostAsync<SessionPayload>(createSessionPath, postPayload).Process(payload => {
				Token = payload.Token;
				_client.SetToken(Token);

			  var user = JsonConvert.DeserializeObject<User>(payload.User);
              var newPayload = new Dictionary<string, object>{
			        {"Token",payload.Token},
			        {"User", user}
			   };


				return newPayload;
			});
        }
    }
}
