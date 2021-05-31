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
    }
}
