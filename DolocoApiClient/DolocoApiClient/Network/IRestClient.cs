using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolocoApiClient.Network
{
    public interface IRestClient
    {
		void SetToken (string token);
        Task<Response<TResponsePayload>> GetAsync<TResponsePayload>(string url);
        Task<Response<TResponsePayload>> PostAsync<TResponsePayload>(string url, object payload);
        Task<Response<TResponsePayload>> DeleteAsync<TResponsePayload>(string url);
        Task<Response<TResponsePayload>> PutAsync<TResponsePayload>(string url, object payload);
    }
}
