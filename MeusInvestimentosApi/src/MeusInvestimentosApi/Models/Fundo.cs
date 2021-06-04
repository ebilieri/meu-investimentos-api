using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MeusInvestimentosApi.Models
{
    /// <summary>
    /// Class to handle with Fundos de investimentos
    /// </summary>
    public partial class Fundo
    {
        /// <summary>
        /// A List items Fundos
        /// </summary>
        [JsonProperty("fundos")]
        public List<FundoItem> Fundos { get; set; }
    }

    /// <summary>
    /// Class to handler with itens de fundos
    /// </summary>
    public partial class FundoItem
    {
        /// <summary>
        /// Capital Investido
        /// </summary>
        [JsonProperty("capitalInvestido")]
        public decimal CapitalInvestido { get; set; }

        /// <summary>
        /// Valor Atual
        /// </summary>
        [JsonProperty("ValorAtual")]
        public decimal ValorAtual { get; set; }

        /// <summary>
        /// Data de Resgate
        /// </summary>
        [JsonProperty("dataResgate")]
        public DateTimeOffset DataResgate { get; set; }

        /// <summary>
        /// Data de Compra
        /// </summary>
        [JsonProperty("dataCompra")]
        public DateTimeOffset DataCompra { get; set; }

        /// <summary>
        /// IOF
        /// </summary>
        [JsonProperty("iof")]
        public decimal Iof { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [JsonProperty("nome")]
        public string Nome { get; set; }

        /// <summary>
        /// Total de Taxas
        /// </summary>
        [JsonProperty("totalTaxas")]
        public decimal TotalTaxas { get; set; }

        /// <summary>
        /// Quantidade
        /// </summary>
        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }
    }
}
