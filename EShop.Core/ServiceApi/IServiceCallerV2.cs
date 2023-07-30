using System.Collections.Generic;
using System.Net.Http;

namespace EShop.Core.ServiceApi
{
    public interface IServiceCallerV2
    {
        ResponseApiCaller Call(HttpMethod httpMethod, KeyValuePair<string, object>[] headers, KeyValuePair<string, object>[] parameters, object contentData);
    }
    public class ResponseApiCaller
    {
        public bool IsSuccess { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
}