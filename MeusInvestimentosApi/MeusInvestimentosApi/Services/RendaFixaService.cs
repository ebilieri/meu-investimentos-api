using MeusInvestimentosApi.Models;
using Microsoft.Extensions.Options;
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
        private readonly ICacheService _cache;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="httpClient"></param>
        /// <param name="cache"></param>
        public RendaFixaService(IOptions<ConfigApi> config,
                                HttpClient httpClient,
                                ICacheService cache) : base(httpClient)
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
            var rendaFixa = await _cache.GetFromCacheOrSource(cacheKey, () =>
            {
                return ObterRendaFixaCalculadoLCI();
            });

            return rendaFixa;
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
