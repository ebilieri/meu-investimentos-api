using MeusInvestimentosApi.Services;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MeusInvestimentosApi.Controllers
{
    /// <summary>
    /// DashBoard Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class InvestimentosController : ControllerBase
    {
        private readonly IInvestimentosFactory _investimentosFactory;

        private readonly TelemetryClient _telemetry;
        

        public InvestimentosController(IInvestimentosFactory investimentosFactory,
                                        TelemetryClient telemetry)
        {
            _investimentosFactory = investimentosFactory;
            _telemetry = telemetry;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            _telemetry.TrackEvent("ObterInvestimentos");
            var invetimentos = await _investimentosFactory.ObterInvestimentos();
            return Ok(invetimentos);
        }
    }
}
