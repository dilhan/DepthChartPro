using DepthChartPro.BL.Interfaces;
using DepthChartPro.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DepthChartPro.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepthChartController : ControllerBase
    {
        private readonly IDepthChartService _depthChartService;
        private readonly ILogger<DepthChartController> _logger;

        public DepthChartController(IDepthChartService depthChartService, ILogger<DepthChartController> logger)
        {
            _depthChartService = depthChartService;
            _logger = logger;
        }

        [Route("/RemovePlayerFromDepthChart/{position}/{playerId}")]
        [HttpDelete]
        public async Task<IActionResult> RemovePlayerFromDepthChart(string position, int playerId)
        {
            var palyer = await _depthChartService.RemovePlayerFromDepthChart(position, playerId);
            return Ok(palyer);
        }

        [Route("/GetBackups/{position}/{playerId}")]
        [HttpGet]
        public async Task<IActionResult> GetBackups(string position, int playerId)
        {
            var backups = await _depthChartService.GetBackups(position, playerId);
            return Ok(backups);
        }

        [Route("/GetFullDepthChart")]
        [HttpGet]
        public async Task<IActionResult> GetFullDepthChart()
        {
           var depthChart = await _depthChartService.GetFullDepthChart();
            return Ok(depthChart);
        }
    }
}
