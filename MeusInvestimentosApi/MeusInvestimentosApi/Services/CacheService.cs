using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace MeusInvestimentosApi.Services
{
    public class CacheService : ICacheService
    {
        private readonly ConfigApi _config;
        private readonly IMemoryCache _cache;


        public CacheService(IOptions<ConfigApi> config,
                            IMemoryCache cache)
        {
            _cache = cache;
            _config = config?.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="funcCache"></param>
        /// <returns></returns>
        public async Task<T> GetFromCacheOrSource<T>(string key, Func<Task<T>> funcCache)
        {
            var cacheEntry = _cache.GetOrCreateAsync(key, f =>
            {
                f.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_config.AbsoluteExpirationRelativeToNow);
                f.SlidingExpiration = TimeSpan.FromMinutes(_config.SlidingExpiration);
                f.Priority = CacheItemPriority.High;

                return funcCache();
            });

            if (await cacheEntry == null)
            {
                _cache.Remove(key);
            }

            return await cacheEntry;
        }
    }
}
