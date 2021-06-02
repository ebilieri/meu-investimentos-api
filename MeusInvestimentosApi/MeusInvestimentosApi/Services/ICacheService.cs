using System;
using System.Threading.Tasks;

namespace MeusInvestimentosApi.Services
{
    public interface ICacheService
    {
        Task<T> GetFromCacheOrSource<T>(string key, Func<Task<T>> funcCache);
    }
}