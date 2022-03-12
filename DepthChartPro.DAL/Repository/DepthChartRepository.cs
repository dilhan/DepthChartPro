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
        public DepthChartRepository(DepthChartContext context)
        {
            _context = context;
        }

        public async Task<Player> RemovePlayerFromDepthChart(string position, int playerId, int? sportId =1,int? TeamId =1)
        {
            var depthChart = await getDepthChart((int)sportId, (int)TeamId);
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
        public async Task<IEnumerable<Player>> GetBackups(string postion, int playerId, int? sportId = 1, int? TeamId = 1)
        {
            var depthChart = await getDepthChart((int)sportId, (int)TeamId);
            if (string.IsNullOrEmpty(postion) && depthChart.PositionDepthQueueList.Count(pq => pq.Position.Code == postion && pq.Player.Number == playerId) == 0) return Enumerable.Empty<Player>();
            var playerPostion = depthChart.PositionDepthQueueList.FirstOrDefault(pq => pq.Position.Code == postion && pq.Player.Number == playerId).PositionDepth;
            var backupPlayers = new List<Player>();
            foreach (var posQueue in depthChart.PositionDepthQueueList.Where(pq => pq.Position.Code == postion && pq.PositionDepth > playerPostion).ToList())
            {
                backupPlayers.Add(posQueue.Player);
            }
            return backupPlayers;
        }

        public async Task<DepthChart> GetFullDepthChart(int? sportId = 1, int? TeamId = 1)
        {
            return await getDepthChart((int)sportId, (int)TeamId);
        }

        private async Task<DepthChart> getDepthChart(int sportId, int TeamId )
        {
            return await _context.DepthChart.
               Include(dc => dc.PositionDepthQueueList)
                   .ThenInclude(pc => pc.Position)
               .Include(dc => dc.PositionDepthQueueList)
                   .ThenInclude(pc => pc.Player)
               .Include(dc => dc.Sport)
               .Include(dc => dc.Team)
            .FirstOrDefaultAsync(dc => dc.Sport.Id == sportId && dc.Team.Id == TeamId);
        }

    }
}
