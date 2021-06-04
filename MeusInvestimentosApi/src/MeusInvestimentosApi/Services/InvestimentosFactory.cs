using MeusInvestimentosApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeusInvestimentosApi.Services
{
    public class InvestimentosFactory : IInvestimentosFactory
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

            var tesouro = await _tesouroDiretoService.ObterTesouroDireto();
            var rendaFixa = await _rendaFixaService.ObterRendaFixa();
            var fundos = await _fundosDiretoService.ObterFundos();

            Investimento investimento = new Investimento
            {
                Investimentos = new List<InvestimentoItem>(),
                ValorTotal = tesouro.ValorTotal + rendaFixa.ValorTotal + fundos.ValorTotal

            };

            investimento.Investimentos.AddRange(tesouro.Investimentos);
            investimento.Investimentos.AddRange(rendaFixa.Investimentos);
            investimento.Investimentos.AddRange(fundos.Investimentos);


            return investimento;
        }
    }
}
