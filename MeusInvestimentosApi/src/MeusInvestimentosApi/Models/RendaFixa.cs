using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MeusInvestimentosApi.Models
{
    /// <summary>
    /// Class to handle with Renda Fixa
    /// </summary>
    public class RendaFixa
    {
        /// <summary>
        /// A list items LCIs
        /// </summary>
        [JsonProperty("lcis")]
        public List<RendaFixaLci> Lcis { get; set; }
    }

    /// <summary>
    /// Class to handle with LCI
    /// </summary>
    public class RendaFixaLci
    {
        /// <summary>
        /// Capital investido
        /// </summary>
        [JsonProperty("capitalInvestido")]
        public decimal CapitalInvestido { get; set; }

        /// <summary>
        /// Capital atual
        /// </summary>
        [JsonProperty("capitalAtual")]
        public decimal CapitalAtual { get; set; }

        /// <summary>
        /// Quantidade
        /// </summary>
        [JsonProperty("quantidade")]
        public decimal Quantidade { get; set; }


        /// <summary>
        /// Vencimento
        /// </summary>
        [JsonProperty("vencimento")]
        public DateTimeOffset Vencimento { get; set; }

        /// <summary>
        /// IOF
        /// </summary>
        [JsonProperty("iof")]
        public decimal Iof { get; set; }

        /// <summary>
        /// Outras Taxas
        /// </summary>
        [JsonProperty("outrasTaxas")]
        public decimal OutrasTaxas { get; set; }

        /// <summary>
        /// Taxas
        /// </summary>
        [JsonProperty("taxas")]
        public decimal Taxas { get; set; }

        /// <summary>
        /// Indice
        /// </summary>
        [JsonProperty("indice")]
        public string Indice { get; set; }

        /// <summary>
        /// Tipo
        /// </summary>
        [JsonProperty("tipo")]
        public string Tipo { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [JsonProperty("nome")]
        public string Nome { get; set; }

        /// <summary>
        /// Garantido pleo FGC
        /// </summary>
        [JsonProperty("guarantidoFGC")]
        public bool GuarantidoFgc { get; set; }

        /// <summary>
        /// Data da operação
        /// </summary>
        [JsonProperty("dataOperacao")]
        public DateTimeOffset DataOperacao { get; set; }

        /// <summary>
        /// Preço Unitario
        /// </summary>
        [JsonProperty("precoUnitario")]
        public decimal PrecoUnitario { get; set; }

        /// <summary>
        /// Primario
        /// </summary>
        [JsonProperty("primario")]
        public bool Primario { get; set; }
    }


}
