using DepthChartPro.BL.Interfaces;
using DepthChartPro.DAL.Interfaces.Repository;
using DepthChartPro.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepthChartPro.BL.Services
{
    public class DepthChartService : IDepthChartService
    {
        private readonly IDepthChartRepository _depthChartRepository;
                
        public DepthChartService(IDepthChartRepository depthChartRepository)
        {
            _depthChartRepository = depthChartRepository;
        }

        public async Task<Player> RemovePlayerFromDepthChart(string position, int playerId)
        {
            return await _depthChartRepository.RemovePlayerFromDepthChart(position, playerId);
        }
        public async Task<IEnumerable<Player>> GetBackups(string position, int playerId)
        {
            return await _depthChartRepository.GetBackups(position, playerId);
        }
        public async Task<DepthChart> GetFullDepthChart()
        {
            return await _depthChartRepository.GetFullDepthChart();
        }

        
    }
}
