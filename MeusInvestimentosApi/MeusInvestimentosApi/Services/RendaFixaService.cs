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
        public async Task<Investimento> ObterRendaFixa()
        {
            string cacheKey = $"ObterRendaFixa";
            var cacheEntry = _cache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_config.AbsoluteExpirationRelativeToNow);
                entry.SlidingExpiration = TimeSpan.FromMinutes(_config.SlidingExpiration);
                entry.Priority = CacheItemPriority.High;

                return ObterRendaFixaCalculadoLCI();
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
        private async Task<Investimento> ObterRendaFixaCalculadoLCI()
        {
            Investimento investimento = new Investimento
            {
                Investimentos = new List<InvestimentoItem>()
            };

            RendaFixa rendaFixa = await ObterInvestimento(_config.RendaFixaBaseURL);
            if (rendaFixa != null && rendaFixa.Lcis?.Count > 0)
            {
                var list = rendaFixa?.Lcis;
                foreach (var item in list)
                {
                    // Redimento
                    var redimento = item.CapitalAtual - item.CapitalInvestido;
                    // calculoIR
                    var IR = CalcularIR(redimento, _config.TaxasIR.LCI);
                    // Valor Resgate
                    var valorResgate = CalcularValorResgate(item.Vencimento.DateTime, item.DataOperacao.DateTime, item.CapitalAtual);

                    var investimentoItem = new InvestimentoItem
                    {
                        Nome = item.Nome,
                        ValorInvestido = item.CapitalInvestido,
                        ValorTotal = item.CapitalAtual,
                        Vencimento = item.Vencimento.ToString("yyyy-MM-ddTHH:mm:ss"),
                        Ir = IR,
                        ValorResgate = valorResgate
                    };

                    // adicionar investimento a lista
                    investimento.Investimentos.Add(investimentoItem);
                    investimento.ValorTotal += item.CapitalAtual;
                }
            }

            return investimento;
        }
    }
}
