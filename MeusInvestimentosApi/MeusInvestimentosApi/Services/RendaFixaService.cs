using MeusInvestimentosApi.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MeusInvestimentosApi.Services
{
    /// <summary>
    /// Class to handler with Tesouro direto
    /// </summary>
    public class RendaFixaService : BaseService<RendaFixa>, IRendaFixaService
    {
        private readonly ConfigApi _config;
        private readonly IMemoryCache _cache;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="httpClient"></param>
        /// <param name="cache"></param>
        public RendaFixaService(IOptions<ConfigApi> config,
                                    HttpClient httpClient,
                                    IMemoryCache cache) : base(config, httpClient)
        {
            _cache = cache;
            _config = config?.Value;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<RendaFixa> ObterRendaFixa()
        {
            string cacheKey = $"ObterRendaFixa";
            var cacheEntry = _cache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_config.AbsoluteExpirationRelativeToNow);
                entry.SlidingExpiration = TimeSpan.FromMinutes(_config.SlidingExpiration);
                entry.Priority = CacheItemPriority.High;

                return ObterInvestimento(_config.RendaFixaBaseURL);
            });

            if (await cacheEntry == null)
            {
                _cache.Remove(cacheKey);
            }

            return await cacheEntry;
        }
    }
}
