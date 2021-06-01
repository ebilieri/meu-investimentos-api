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
        public async Task<Investimento> ObterFundos()
        {
            string cacheKey = $"ObterFundos";
            var cacheEntry = _cache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_config.AbsoluteExpirationRelativeToNow);
                entry.SlidingExpiration = TimeSpan.FromMinutes(_config.SlidingExpiration);
                entry.Priority = CacheItemPriority.High;

                return ObterFundosCalculado();
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
        private async Task<Investimento> ObterFundosCalculado()
        {
            Investimento investimento = new Investimento
            {
                Investimentos = new List<InvestimentoItem>()
            };

            Fundo fundos = await ObterInvestimento(_config.FundosBaseURL);
            if (fundos != null && fundos.Fundos?.Count > 0)
            {
                var list = fundos?.Fundos;
                foreach (var item in list)
                {
                    // Redimento
                    var redimento = item.ValorAtual - item.CapitalInvestido;
                    // calculoIR
                    var IR = CalcularIR(redimento, _config.TaxasIR.Fundos);
                    // Valor Resgate
                    var valorResgate = CalcularValorResgate(item.DataResgate.DateTime, item.DataCompra.DateTime, item.ValorAtual);

                    var investimentoItem = new InvestimentoItem
                    {
                        Nome = item.Nome,
                        ValorInvestido = item.CapitalInvestido,
                        ValorTotal = item.ValorAtual,
                        Vencimento = item.DataResgate.ToString("yyyy-MM-ddTHH:mm:ss"),
                        Ir = IR,
                        ValorResgate = valorResgate
                    };

                    // adicionar investimento a lista
                    investimento.Investimentos.Add(investimentoItem);
                    investimento.ValorTotal += item.ValorAtual;
                }
            }

            return investimento;
        }
    }
}
