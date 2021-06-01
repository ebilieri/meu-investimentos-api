using MeusInvestimentosApi.Models;
using System;
using System.Threading.Tasks;

namespace MeusInvestimentosApi.Services
{
    public class InvestimentosFactory
    {
        private readonly ITesouroDiretoService _tesouroDiretoService;
        private readonly IRendaFixaService _rendaFixaService;
        private readonly IFundosService _fundosDiretoService;

        public InvestimentosFactory(ITesouroDiretoService tesouroDiretoService,
                                    IRendaFixaService rendaFixaService,
                                    IFundosService fundosDiretoService)
        {
            _tesouroDiretoService = tesouroDiretoService;
            _rendaFixaService = rendaFixaService;
            _fundosDiretoService = fundosDiretoService;
        }


        public async Task<Investimento> ObterInvestimentos()
        {

            var tesouro = await _tesouroDiretoService.ObterTesouroDiretoCalculado();
            var rendaFixa = await _rendaFixaService.ObterRendaFixa();
            var fundos = await _fundosDiretoService.ObterFundos();

            throw new NotImplementedException();
        }
    }
}
