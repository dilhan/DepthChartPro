using DepthChartPro.DAL.DataAccess;
using DepthChartPro.DAL.Interfaces.Repository;
using DepthChartPro.DAL.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task AddPlayerToDepthChart(string position, int playerId, int positionDepth)
        {
            if (string.IsNullOrEmpty(position))
                throw new System.ArgumentNullException("position", "Parameter is null or empty");

            var depthChart = await getDepthChart();
            var positionDepthQueue = new PositionDepthQueue 
            { 
                Position = await _context.Positions.FirstOrDefaultAsync(p => p.Code == position), 
                Player = await _context.Players.FirstOrDefaultAsync(p=>p.Number == playerId), 
                PositionDepth = positionDepth
            };
            depthChart.PositionDepthQueueList.Add(positionDepthQueue);
            _ = _context.SaveChangesAsync();
        }
        public async Task RemovePlayerFromDepthChart(int removeIndex)
        {
            var depthChart = await getDepthChart();
            depthChart.PositionDepthQueueList.RemoveAt(removeIndex);
            _ = _context.SaveChangesAsync();
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
