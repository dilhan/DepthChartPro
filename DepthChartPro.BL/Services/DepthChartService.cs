using DepthChartPro.BL.Interfaces;
using DepthChartPro.DAL.Interfaces.Repository;
using DepthChartPro.DAL.Models;
using System.Collections.Generic;
using System.Linq;
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

        public async Task AddPlayerToDepthChart(string position, int playerId, int? positionDepth)
        {
            if (string.IsNullOrEmpty(position))
                throw new System.ArgumentNullException("position", "Parameter is null or empty");

            var depthChart = await GetFullDepthChart();
            if (positionDepth == null && depthChart.PositionDepthQueueList.Any(pq => pq.Position.Code == position))
            {
                var lastpositionDepth = depthChart.PositionDepthQueueList.FindLast(dc => dc.Position.Code == position).PositionDepth + 1;
                await _depthChartRepository.AddPlayerToDepthChart(position, playerId, lastpositionDepth);
            }
            else
            {
                var posDepth = (int)positionDepth;
                if (depthChart.PositionDepthQueueList.FindAll(dc => dc.Position.Code == position && dc.PositionDepth == posDepth).Count > 0)
                {
                    foreach (var depthChartItem in depthChart.PositionDepthQueueList.FindAll(dc => dc.Position.Code == position))
                    {
                        if (posDepth >= depthChartItem.PositionDepth)
                        {
                            depthChartItem.PositionDepth++;
                        }
                    }
                }
                await _depthChartRepository.AddPlayerToDepthChart(position, playerId, posDepth);
            }
        }
        public async Task<Player> RemovePlayerFromDepthChart(string position, int playerId)
        {
            if (string.IsNullOrEmpty(position))
                throw new System.ArgumentNullException("position", "Parameter is null or empty");

            var depthChart = await GetFullDepthChart();
            if (depthChart.PositionDepthQueueList.Any(pq => pq.Position.Code == position && pq.Player.Number == playerId))
            {
                var removeIndex = depthChart.PositionDepthQueueList.FindIndex(pq => pq.Position.Code == position && pq.Player.Number == playerId);
                var player = depthChart.PositionDepthQueueList.FirstOrDefault(pq => pq.Position.Code == position && pq.Player.Number == playerId).Player;
                await _depthChartRepository.RemovePlayerFromDepthChart(removeIndex);
                return player;
            }
            return null;
        }
        public async Task<IEnumerable<Player>> GetBackups(string position, int playerId)
        {
            if (string.IsNullOrEmpty(position))
                throw new System.ArgumentNullException("position", "Parameter is null or empty");

            var depthChart = await GetFullDepthChart();
            if (string.IsNullOrEmpty(position) && depthChart.PositionDepthQueueList.Count(pq => pq.Position.Code == position && pq.Player.Number == playerId) == 0) return Enumerable.Empty<Player>();
            var playerPostion = depthChart.PositionDepthQueueList.FirstOrDefault(pq => pq.Position.Code == position && pq.Player.Number == playerId).PositionDepth;
            var backupPlayers = new List<Player>();
            foreach (var posQueue in depthChart.PositionDepthQueueList.Where(pq => pq.Position.Code == position && pq.PositionDepth > playerPostion).ToList())
            {
                backupPlayers.Add(posQueue.Player);
            }
            return backupPlayers;
        }
        public async Task<DepthChart> GetFullDepthChart()
        {
            var depthChart = await _depthChartRepository.GetFullDepthChart();
            depthChart.PositionDepthQueueList = depthChart.PositionDepthQueueList.OrderBy(pq => pq.Position.Id).ThenBy(pq => pq.PositionDepth).ToList();
            return depthChart;
        }

        
    }
}
