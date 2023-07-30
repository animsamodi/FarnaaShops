using System.Net.Http;

namespace EShop.Core.ServiceApi
{
    public interface IServiceCaller
    {
        object Call(HttpMethod httpMethod, params object[] parameters);
    }
}