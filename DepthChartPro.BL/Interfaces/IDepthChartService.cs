using DepthChartPro.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepthChartPro.BL.Interfaces
{
    public interface IDepthChartService
    {
        /// <summary>
        /// Removed player from Depth Chart
        /// </summary>
        /// <param name="position"></param>
        /// <param name="playerId"></param>
        /// <returns>Player</returns>
        public Task<Player> RemovePlayerFromDepthChart(string position, int playerId);
        /// <summary>
        /// Get all backup players
        /// </summary>
        /// <param name="position"></param>
        /// <param name="playerId"></param>
        /// <returns>IEnumerable<Player></returns>
        public Task<IEnumerable<Player>> GetBackups(string position, int playerId);
        /// <summary>
        /// Get full Depth Chart
        /// </summary>
        /// <returns>DepthChart</returns>
        public Task<DepthChart> GetFullDepthChart();
    }
}
