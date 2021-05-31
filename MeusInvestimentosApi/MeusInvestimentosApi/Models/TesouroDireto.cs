using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MeusInvestimentosApi.Models
{
    /// <summary>
    /// Class to handle with Tesouro Direto
    /// </summary>
    public class TesouroDireto
    {
        /// <summary>
        /// A List Tesouro direto
        /// </summary>
        [JsonProperty("tds")]
        public List<TesouroDiretoItem> Tds { get; set; }
    }

    /// <summary>
    /// Class to handle with Tesouro Direto Items
    /// </summary>
    public class TesouroDiretoItem
    {
        /// <summary>
        /// Valor Investido
        /// </summary>
        [JsonProperty("valorInvestido")]
        public decimal ValorInvestido { get; set; }

        /// <summary>
        /// Valor Total
        /// </summary>
        [JsonProperty("valorTotal")]
        public decimal ValorTotal { get; set; }

        /// <summary>
        /// Data de Vencimento
        /// </summary>
        [JsonProperty("vencimento")]
        public DateTimeOffset Vencimento { get; set; }

        /// <summary>
        /// Data de Compra
        /// </summary>
        [JsonProperty("dataDeCompra")]
        public DateTimeOffset DataDeCompra { get; set; }

        /// <summary>
        /// IOF
        /// </summary>
        [JsonProperty("iof")]
        public decimal Iof { get; set; }

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
    }
}
