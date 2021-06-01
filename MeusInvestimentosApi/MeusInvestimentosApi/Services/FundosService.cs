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
    public class FundosService : BaseService<Fundo>, IFundosService
    {
        private readonly ConfigApi _config;
        private readonly IMemoryCache _cache;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="httpClient"></param>
        /// <param name="cache"></param>
        public FundosService(IOptions<ConfigApi> config,
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
        public async Task<Fundo> ObterFundos()
        {
            string cacheKey = $"ObterFundos";
            var cacheEntry = _cache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_config.AbsoluteExpirationRelativeToNow);
                entry.SlidingExpiration = TimeSpan.FromMinutes(_config.SlidingExpiration);
                entry.Priority = CacheItemPriority.High;

                return ObterInvestimento(_config.FundosBaseURL);
            });

            if (await cacheEntry == null)
            {
                _cache.Remove(cacheKey);
            }

            return await cacheEntry;
        }
    }
}
