using System;
using System.Threading.Tasks;

namespace MeusInvestimentosApi.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="funcCache"></param>
        /// <returns></returns>
        Task<T> GetFromCacheOrSource<T>(string key, Func<Task<T>> funcCache);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheKey"></param>
        void Remove(string cacheKey);
    }
}