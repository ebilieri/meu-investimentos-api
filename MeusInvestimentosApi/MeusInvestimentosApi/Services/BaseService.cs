using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MeusInvestimentosApi.Services
{
    /// <summary>
    /// Class Base service
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseService<TEntity> where TEntity : class
    {
        private readonly HttpClient _httpClient;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="httpClient"></param>
        public BaseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        /// <summary>
        /// Obter lista de rend fixa
        /// </summary>
        /// <returns></returns>
        protected async Task<TEntity> ObterInvestimento(string uri)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri)
            };

            var response = await _httpClient.SendAsync(request);
            string result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var entity = JsonConvert.DeserializeObject<TEntity>(result);
                return entity;
            }

            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="redimento"></param>
        /// <param name="taxa"></param>
        /// <returns></returns>
        protected decimal CalcularIR(decimal redimento, decimal taxa)
        {
            return redimento * taxa / 100;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vencimento"></param>
        /// <param name="dataDeCompra"></param>
        /// <param name="valorTotal"></param>
        /// <returns></returns>
        protected decimal CalcularValorResgate(DateTime vencimento, DateTime dataDeCompra, decimal valorTotal)
        {
            var tempoAteVencimento = (vencimento.Date - dataDeCompra.Date).TotalDays;
            var tempoAplicacao = (DateTime.Now.Date - dataDeCompra.Date).TotalDays;
            var tempoResgate = tempoAteVencimento - tempoAplicacao;

            decimal menosMetadeTempoCustodia = 30;
            decimal maisMetadeTempoCustodia = 15;
            decimal ateTreseMeses = 6;

            decimal valorDescontoResgate;

            if (tempoResgate <= 0)
            {
                valorDescontoResgate = 0;
            }
            else if (tempoResgate <= 90)
            {
                valorDescontoResgate = valorTotal * ateTreseMeses / 100;
            }
            else if (tempoAplicacao > tempoAteVencimento / 2)
            {
                valorDescontoResgate = valorTotal * maisMetadeTempoCustodia / 100;
            }
            else
            {
                valorDescontoResgate = valorTotal * menosMetadeTempoCustodia / 100;
            }

            return valorTotal - valorDescontoResgate;
        }
    }
}
