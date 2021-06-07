using MeusInvestimentosApi.Models;
using MeusInvestimentosApi.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        private readonly ICacheService _cache;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="httpClient"></param>
        /// <param name="logger"></param>
        /// <param name="cache"></param>
        public TesouroDiretoService(IOptions<ConfigApi> config,
                                    HttpClient httpClient,
                                    ILogger<TesouroDireto> logger,
                                    ICacheService cache) : base(httpClient, logger)
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
            var tesouro = await _cache.GetFromCacheOrSource(cacheKey, () =>
            {
                return ObterTesouroDiretoCalculado();
            });

            //if (tesouro.Investimentos?.Count == 0) _cache.Remove(cacheKey);

            return tesouro;
        }


        // Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Investimento> ObterTesouroDiretoCalculado()
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
