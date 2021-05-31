using System.Collections.Generic;

namespace MeusInvestimentosApi.Models
{
    /// <summary>
    /// Class to handle with investimentos
    /// </summary>
    public class Investimento
    {
        /// <summary>
        /// Valor Total Investido
        /// </summary>
        public decimal ValorTotal { get; set; }

        /// <summary>
        /// A List items Investimentos
        /// </summary>
        public List<InvestimentoItem> Investimentos { get; set; }
    }

    /// <summary>
    /// Class to handle with Investimentos items
    /// </summary>
    public class InvestimentoItem
    {
        /// <summary>
        /// Nome Investimento
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Valor Investido
        /// </summary>
        public decimal ValorInvestido { get; set; }

        /// <summary>
        /// Valor Total
        /// </summary>
        public decimal ValorTotal { get; set; }

        /// <summary>
        /// Data de vencimento
        /// </summary>
        public string Vencimento { get; set; }

        /// <summary>
        /// IR
        /// </summary>
        public decimal Ir { get; set; }

        /// <summary>
        /// Valor de Resgate
        /// </summary>
        public decimal ValorResgate { get; set; }
    }
}
