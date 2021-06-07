using FluentAssertions;
using MeusInvestimentos.UnitTest.Fixtures;
using MeusInvestimentosApi.Models;
using MeusInvestimentosApi.Services;
using MeusInvestimentosApi.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MeusInvestimentos.UnitTest.Services
{
    public class TesouroDiretoServiceTest : IClassFixture<ConfigFixture>
    {
        private readonly Mock<ILogger<TesouroDireto>> _logger;
        private readonly Mock<ICacheService> _cache;

        private readonly ConfigFixture _configFixture;
        private readonly HttpClient _httpClient;
        private readonly ITesouroDiretoService _tesouroDiretoService;

        

        public TesouroDiretoServiceTest(ConfigFixture configFixture)
        {
            _logger = new Mock<ILogger<TesouroDireto>>();
            _cache = new Mock<ICacheService>();
            _configFixture = configFixture;
            var settings = _configFixture.LoadConfig();
            _httpClient = new HttpClient();

            _tesouroDiretoService = new TesouroDiretoService(settings, _httpClient, _logger.Object, _cache.Object);            
        }

        [Fact]
        public void TesouroDiretoService_ObterTesouroDireto_Sucesso()
        {
            Investimento investimento = new Investimento
            {
                ValorTotal = 705.228M,
                Investimentos = new List<InvestimentoItem>()
            };

            var investimentoItem = new InvestimentoItem
            {
                Nome = "Tesouro Selic 2025",
                ValorInvestido = 799.472M,
                ValorTotal = 829.68M,
                Vencimento = "2025-03-01T00:00:00",
                Ir = 3.0208M,
                ValorResgate = 705.228M
            };

            investimento.Investimentos.Add(investimentoItem);
            _cache.Setup(x => x.GetFromCacheOrSource("ObterTesouroDireto", It.IsAny<Func<Task<Investimento>>>())).ReturnsAsync(investimento);


            var retorno = _tesouroDiretoService.ObterTesouroDireto().Result;

            retorno.ValorTotal.Should().Be(investimento.ValorTotal);
            retorno.Investimentos[0].Nome.Should().Be(investimento.Investimentos[0].Nome);
            retorno.Investimentos[0].ValorInvestido.Should().Be(investimento.Investimentos[0].ValorInvestido);
        }


        



    }
}
