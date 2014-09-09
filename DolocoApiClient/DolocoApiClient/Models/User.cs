using System;
using Newtonsoft.Json;

namespace DolocoApiClient.Models
{
	public class User
	{
	    [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("first_name")]
		public string FirstName { get; set; }
        [JsonProperty("last_name")]
		public string LastName { get; set; }
        [JsonProperty("email")]
		public string Email { get; set; }
	}
}

