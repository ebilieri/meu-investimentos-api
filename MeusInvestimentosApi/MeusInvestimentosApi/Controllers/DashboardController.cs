using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeusInvestimentosApi.Controllers
{
    public class DashboardController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
