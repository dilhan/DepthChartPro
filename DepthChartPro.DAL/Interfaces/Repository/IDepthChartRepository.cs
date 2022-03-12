using DepthChartPro.DAL.Models;
using System.Threading.Tasks;

namespace DepthChartPro.DAL.Interfaces.Repository
{
    public interface IDepthChartRepository
    {
        /// <summary>
        /// Add player from Depth Chart
        /// </summary>
        /// <param name="position"></param>
        /// <param name="playerId"></param>
        /// <param name="positionDepth"></param>
        /// <returns></returns>
        public Task AddPlayerToDepthChart(string position, int playerId, int positionDepth);
        /// <summary>
        /// Remove player from Depth Chart
        /// </summary>
        /// <param name="removeIndex"></param>
        /// <returns>Player</returns>
        public Task RemovePlayerFromDepthChart(int removeIndex);
        /// <summary>
        /// Get full Depth Chart
        /// </summary>
        /// <returns>DepthChart</returns>
        public Task<DepthChart> GetFullDepthChart();
    }
}
