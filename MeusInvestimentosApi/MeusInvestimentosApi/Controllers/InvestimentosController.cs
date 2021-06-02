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
    public class InvestimentosController : ControllerBase
    {
        private readonly IInvestimentosFactory _investimentosFactory;
        

        public InvestimentosController(IInvestimentosFactory investimentosFactory)
        {
            _investimentosFactory = investimentosFactory;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var invetimentos = await _investimentosFactory.ObterInvestimentos();
            return Ok(invetimentos);
        }
    }
}
