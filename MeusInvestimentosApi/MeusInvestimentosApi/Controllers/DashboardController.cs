using MeusInvestimentosApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MeusInvestimentosApi.Controllers
{
    /// <summary>
    /// DashBoard Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ITesouroDiretoService _tesouroDiretoService;
        private readonly IRendaFixaService _rendaFixaService;
        private readonly IFundosService _fundosDiretoService;

        public DashboardController(ITesouroDiretoService tesouroDiretoService,
                                    IRendaFixaService rendaFixaService,
                                    IFundosService fundosDiretoService)
        {
            _tesouroDiretoService = tesouroDiretoService;
            _rendaFixaService = rendaFixaService;
            _fundosDiretoService = fundosDiretoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tesouro = await _tesouroDiretoService.ObterTesouroDireto();
            var rendaFixa = await _rendaFixaService.ObterRendaFixa();
            var fundos = await _fundosDiretoService.ObterFundos();
            return Ok(tesouro);
        }
    }
}
