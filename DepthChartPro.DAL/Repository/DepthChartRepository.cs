using DepthChartPro.DAL.DataAccess;
using DepthChartPro.DAL.Interfaces.Repository;
using DepthChartPro.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepthChartPro.DAL.Repository
{
    public class DepthChartRepository : IDepthChartRepository
    {
        private readonly DepthChartContext _context;
        private readonly int _sportId;
        private readonly int _teamId;
        public DepthChartRepository(DepthChartContext context)
        {
            _context = context;
            _sportId = 1;
            _teamId = 1;
        }

        public async Task AddPlayerToDepthChart(string position, int playerId, int? positionDepth)
        {
            var depthChart = await getDepthChart();
            if (positionDepth == null && depthChart.PositionDepthQueueList.Any(pq => pq.Position.Code == position))
            {
                var lastpositionDepth = depthChart.PositionDepthQueueList.FindLast(dc => dc.Position.Code == position).PositionDepth;
                var positionDepthQueue = new PositionDepthQueue 
                { 
                    Position = new Position { Code = position }, 
                    Player = await _context.Players.FirstOrDefaultAsync(p=>p.Number == playerId), 
                    PositionDepth = lastpositionDepth + 1 
                };
                depthChart.PositionDepthQueueList.Add(positionDepthQueue);
                depthChart.PositionDepthQueueList.OrderBy(pc => pc.Position.Code);
                _ = _context.SaveChangesAsync();
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
                var positionDepthQueue = new PositionDepthQueue
                {
                    Position = new Position { Code = position },
                    Player = await _context.Players.FirstOrDefaultAsync(p => p.Number == playerId),
                    PositionDepth = posDepth
                };
                depthChart.PositionDepthQueueList.Add(positionDepthQueue);
                depthChart.PositionDepthQueueList.OrderBy(pc => pc.Position.Code);
                _ = _context.SaveChangesAsync();
            }
        }
        public async Task<Player> RemovePlayerFromDepthChart(string position, int playerId)
        {
            var depthChart = await getDepthChart();
            if (depthChart.PositionDepthQueueList.Any(pq => pq.Position.Code == position && pq.Player.Number == playerId))
            {
                var removeIndex = depthChart.PositionDepthQueueList.FindIndex(pq => pq.Position.Code == position && pq.Player.Number == playerId);
                var player = depthChart.PositionDepthQueueList.FirstOrDefault(pq => pq.Position.Code == position && pq.Player.Number == playerId).Player;
                depthChart.PositionDepthQueueList.RemoveAt(removeIndex);
                _ = _context.SaveChangesAsync();
                return player;
            }
            return null;
        }
        public async Task<IEnumerable<Player>> GetBackups(string postion, int playerId)
        {
            var depthChart = await getDepthChart();
            if (string.IsNullOrEmpty(postion) && depthChart.PositionDepthQueueList.Count(pq => pq.Position.Code == postion && pq.Player.Number == playerId) == 0) return Enumerable.Empty<Player>();
            var playerPostion = depthChart.PositionDepthQueueList.FirstOrDefault(pq => pq.Position.Code == postion && pq.Player.Number == playerId).PositionDepth;
            var backupPlayers = new List<Player>();
            foreach (var posQueue in depthChart.PositionDepthQueueList.Where(pq => pq.Position.Code == postion && pq.PositionDepth > playerPostion).ToList())
            {
                backupPlayers.Add(posQueue.Player);
            }
            return backupPlayers;
        }

        public async Task<DepthChart> GetFullDepthChart()
        {
            return await getDepthChart();
        }

        private async Task<DepthChart> getDepthChart()
        {
            return await _context.DepthChart.
               Include(dc => dc.PositionDepthQueueList)
                   .ThenInclude(pc => pc.Position) 
               .Include(dc => dc.PositionDepthQueueList)
                   .ThenInclude(pc => pc.Player)
               .Include(dc => dc.PositionDepthQueueList)
               .Include(dc => dc.Sport)
               .Include(dc => dc.Team)
            .FirstOrDefaultAsync(dc => dc.Sport.Id == _sportId && dc.Team.Id == _teamId);
        }

    }
}
