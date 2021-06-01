using MeusInvestimentosApi.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MeusInvestimentosApi.Services
{
    /// <summary>
    /// Class to handler with Tesouro direto
    /// </summary>
    public class TesouroDiretoService : BaseService<TesouroDireto>, ITesouroDiretoService
    {
        private readonly ConfigApi _config;
        private readonly IMemoryCache _cache;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="httpClient"></param>
        /// <param name="cache"></param>
        public TesouroDiretoService(IOptions<ConfigApi> config,
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
        public async Task<Investimento> ObterTesouroDireto()
        {
            string cacheKey = $"ObterTesouroDireto";
            var cacheEntry = _cache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_config.AbsoluteExpirationRelativeToNow);
                entry.SlidingExpiration = TimeSpan.FromMinutes(_config.SlidingExpiration);
                entry.Priority = CacheItemPriority.High;

                return ObterTesouroDiretoCalculado();
            });

            if (await cacheEntry == null)
            {
                _cache.Remove(cacheKey);
            }

            return await cacheEntry;
        }


        // Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<Investimento> ObterTesouroDiretoCalculado()
        {
            Investimento investimento = new Investimento
            {
                Investimentos = new List<InvestimentoItem>()
            };

            TesouroDireto tesouro = await ObterInvestimento(_config.TesouroDiretoBaseURL);
            if (tesouro != null && tesouro.Tds?.Count > 0)
            {
                var list = tesouro?.Tds;
                foreach (var item in list)
                {
                    // Redimento
                    var redimento = item.ValorTotal - item.ValorInvestido;
                    // calculoIR
                    var IR = CalcularIR(redimento, _config.TaxasIR.TesouroDireto);
                    // Valor Resgate
                    var valorResgate = CalcularValorResgate(item.Vencimento.DateTime, item.DataDeCompra.DateTime, item.ValorTotal);

                    var investimentoItem = new InvestimentoItem
                    {
                        Nome = item.Nome,
                        ValorInvestido = item.ValorInvestido,
                        ValorTotal = item.ValorTotal,
                        Vencimento = item.Vencimento.ToString("yyyy-MM-ddTHH:mm:ss"),
                        Ir = IR,
                        ValorResgate = valorResgate
                    };

                    // adicionar investimento a lista
                    investimento.Investimentos.Add(investimentoItem);
                    investimento.ValorTotal += item.ValorTotal;
                }
            }

            return investimento;
        }

    }
}
