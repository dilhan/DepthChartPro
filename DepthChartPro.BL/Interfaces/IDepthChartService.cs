using DepthChartPro.DAL.Models;
using System.Collections.Generic;

namespace DepthChartPro.BL.Interfaces
{
    public interface IDepthChartService
    {
        public Player RemovePlayerFromDepthChart(string position, int playerId);
        public IEnumerable<Player> GetBackups(string postion, int playerId);
        public DepthChart GetFullDepthChart();
    }
}
