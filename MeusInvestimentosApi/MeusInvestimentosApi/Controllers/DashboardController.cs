using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MeusInvestimentosApi.Controllers
{
    /// <summary>
    /// DashBoard Controller
    /// </summary>
    public class DashboardController : ControllerBase
    {
        public async Task<IActionResult> Index()
        {
            return Ok();
        }
    }
}
