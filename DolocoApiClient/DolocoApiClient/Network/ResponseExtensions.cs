using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolocoApiClient.Network
{
    internal static class ResponseExtensions
    {
        public static async Task<TPayload> Process<TPayload>(this Task<Response<TPayload>> responseTask)
        {
            var response = await responseTask;

            return response.Payload;
        }

        public static async Task<TResponse> Process<TPayload, TResponse>(this Task<Response<TPayload>> responseTask,
                                                                         Func<TPayload, TResponse> unwrapper)
        {
            var response = await responseTask.Process();

            return unwrapper(response);
        }
    }
}
