using System.Threading.Tasks;
using AroundTheWorld.Prism.Models;

namespace AroundTheWorld.Prism.Services
{
    public interface IApiService
    {
        Task<bool> CheckConnection(string url);

        Task<Response> GetCountriesInfoAsync<T>(
            string urlBase);
    }
}
