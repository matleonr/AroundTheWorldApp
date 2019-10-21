using System.Threading.Tasks;
using AroundTheWorld.Prism.Models;

namespace AroundTheWorld.Prism.Services
{
    public interface IApiService
    {
        Task<bool> CheckConnection();

        Task<Response> GetCountriesInfoAsync<T>(
            string urlBase);
    }
}
