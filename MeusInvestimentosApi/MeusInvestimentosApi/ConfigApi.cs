namespace MeusInvestimentosApi
{
    /// <summary>
    /// Class to handle with environment variables 
    /// </summary>
    public class ConfigApi
    {
        /// <summary>
        /// Tesouro direto Url
        /// </summary>
        public string TesouroDiretoBaseURL { get; set; }

        /// <summary>
        /// Renda fixa URL
        /// </summary>
        public string RendaFixaBaseURL { get; set; }

        /// <summary>
        /// Fundos URL
        /// </summary>
        public string FundosBaseURL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double AbsoluteExpirationRelativeToNow { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double SlidingExpiration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TaxasIR TaxasIR { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TaxasIR
    {
        /// <summary>
        /// 
        /// </summary>
        public decimal Fundos { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal LCI { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TesouroDireto { get; set; }
    }
}
