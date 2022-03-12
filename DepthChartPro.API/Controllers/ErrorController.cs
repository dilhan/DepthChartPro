using Microsoft.AspNetCore.Mvc;

namespace DepthChartPro.API.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/Error")]
        [HttpGet]
        public IActionResult Error() => Problem();
    }
}
