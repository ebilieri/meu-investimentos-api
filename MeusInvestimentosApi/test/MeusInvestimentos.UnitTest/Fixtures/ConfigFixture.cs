using MeusInvestimentosApi;
using Microsoft.Extensions.Options;

namespace MeusInvestimentos.UnitTest.Fixtures
{
    public class ConfigFixture
    {

        public IOptions<ConfigApi> LoadConfig()
        {
            var settings = new ConfigApi()
            {
                TesouroDiretoBaseURL = "http://www.mocky.io/v2/5e3428203000006b00d9632a",
                TaxasIR = new TaxasIR
                {
                    Fundos = 15,
                    LCI = 5,
                    TesouroDireto = 10
                }
            };

            return Options.Create(settings);
        }
    }
}
