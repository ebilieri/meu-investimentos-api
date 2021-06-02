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
    public class FundosService : BaseService<Fundo>, IFundosService
    {
        private readonly ConfigApi _config;
        private readonly ICacheService _cache;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="httpClient"></param>
        /// <param name="cache"></param>
        public FundosService(IOptions<ConfigApi> config,
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
        public async Task<Investimento> ObterFundos()
        {
            string cacheKey = $"ObterFundos";
            var fundos = await _cache.GetFromCacheOrSource(cacheKey, () =>
            {
                return ObterFundosCalculado();
            });

            return fundos;
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
