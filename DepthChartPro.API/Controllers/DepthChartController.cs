using DepthChartPro.BL.Interfaces;
using DepthChartPro.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DepthChartPro.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepthChartController : ControllerBase
    {
        private readonly IDepthChartService _depthChartService;

        public DepthChartController(IDepthChartService depthChartService)
        {
            _depthChartService = depthChartService;
        }

        [Route("/RemovePlayerFromDepthChart/{position}/{playerId}")]
        [HttpDelete]
        public async Task<IActionResult> RemovePlayerFromDepthChart(string position, int playerId)
        {
            var palyer = _depthChartService.RemovePlayerFromDepthChart(position, playerId);
            return Ok(palyer);
        }

        [Route("/GetBackups/{position}/{playerId}")]
        [HttpGet]
        public async Task<IActionResult> GetBackups(string position, int playerId)
        {
            var backups = _depthChartService.GetBackups(position, playerId);
            return Ok(backups);
        }

        [Route("/GetFullDepthChart")]
        [HttpGet]
        public async Task<IActionResult> GetFullDepthChart()
        {
           var depthChart = _depthChartService.GetFullDepthChart();
            return Ok(depthChart);
        }
    }
}
