using DepthChartPro.API.Models;
using DepthChartPro.BL.Interfaces;
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

        [Route("/AddPlayerToDepthChart")]
        [HttpPost]
        public async Task<IActionResult> AddPlayerToDepthChart([FromBody] AddPlayerModel addPlayerModel)
        {
            if (addPlayerModel == null)
                throw new System.ArgumentNullException("addPlayerModel", "Parameter is null or empty");
            if (string.IsNullOrEmpty(addPlayerModel.position))
                throw new System.ArgumentNullException("position", "Parameter is null or empty");

            await _depthChartService.AddPlayerToDepthChart(addPlayerModel.position, addPlayerModel.playerId, addPlayerModel.positionDepth);
            return Ok();
        }

        [Route("/RemovePlayerFromDepthChart/{position}/{playerId}")]
        [HttpDelete]
        public async Task<IActionResult> RemovePlayerFromDepthChart(string position, int playerId)
        {
            if (string.IsNullOrEmpty(position))
                throw new System.ArgumentNullException("position", "Parameter is null or empty");

            var palyer = await _depthChartService.RemovePlayerFromDepthChart(position, playerId);
            return Ok(palyer);
        }

        [Route("/GetBackups/{position}/{playerId}")]
        [HttpGet]
        public async Task<IActionResult> GetBackups(string position, int playerId)
        {
            if (string.IsNullOrEmpty(position))
                throw new System.ArgumentNullException("position", "Parameter is null or empty");

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
