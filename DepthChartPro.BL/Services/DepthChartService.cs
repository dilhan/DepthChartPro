using DepthChartPro.BL.Interfaces;
using DepthChartPro.DAL.Interfaces.Repository;
using DepthChartPro.DAL.Models;
using System.Collections.Generic;

namespace DepthChartPro.BL.Services
{
    public class DepthChartService : IDepthChartService
    {
        private readonly IDepthChartRepository _depthChartRepository;
                
        public DepthChartService(IDepthChartRepository depthChartRepository)
        {
            _depthChartRepository = depthChartRepository;
        }

        public Player RemovePlayerFromDepthChart(string position, int playerId)
        {
            return _depthChartRepository.RemovePlayerFromDepthChart(position, playerId);
        }
        public IEnumerable<Player> GetBackups(string position, int playerId)
        {
            return _depthChartRepository.GetBackups(position, playerId);
        }
        public DepthChart GetFullDepthChart()
        {
            return _depthChartRepository.GetFullDepthChart();
        }

        
    }
}
