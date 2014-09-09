using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DolocoApiClient.Models;
using DolocoApiClient.Network;
using DolocoApiClient.Payloads;
using Newtonsoft.Json;

namespace DolocoApiClient
{
    public partial class DolocoApiClient
    {
        public Task<string> RegisterUserAsync(string email, string firsName, string lastName, string password,
            string passwordConfirmation)
        {
            var registrationUrl = GetRoutePathUrl(DolocoApiRouteEnum.Registration);
            var registrationPayload = new Dictionary<string, Dictionary<string, string>>
            {
                {
                    "user", new Dictionary<string, string>
                    {
                        {"email", email},
                        {"first_name", firsName},
                        {"last_name", lastName},
                        {"password", password},
                        {"password_confirmation", passwordConfirmation}
                    }
                }
            };

            return
                _client.PostAsync<SessionPayload>(registrationUrl, registrationPayload)
					.Process(payload => {
						Token = payload.Token;
						_client.SetToken(Token);

						return payload.Token;
					});
        }
    }
}
