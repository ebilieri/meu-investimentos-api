using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MeusInvestimentosApi.Models
{
    public class TesouroDireto
    {
        [JsonProperty("tds")]
        public List<TesouroDiretoItem> Tds { get; set; }
    }

    public class TesouroDiretoItem
    {
        [JsonProperty("valorInvestido")]
        public decimal ValorInvestido { get; set; }

        [JsonProperty("valorTotal")]
        public decimal ValorTotal { get; set; }

        [JsonProperty("vencimento")]
        public DateTimeOffset Vencimento { get; set; }

        [JsonProperty("dataDeCompra")]
        public DateTimeOffset DataDeCompra { get; set; }

        [JsonProperty("iof")]
        public decimal Iof { get; set; }

        [JsonProperty("indice")]
        public string Indice { get; set; }

        [JsonProperty("tipo")]
        public string Tipo { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }
    }
}
