using DepthChartPro.DAL.Models;
using System.Collections.Generic;

namespace DepthChartPro.DAL.Interfaces.Repository
{
    public interface IDepthChartRepository
    {
        public Player RemovePlayerFromDepthChart(string position, int playerId, int? sportId = 1, int? TeamId = 1);
        public IEnumerable<Player> GetBackups(string postion, int playerId, int? sportId = 1, int? TeamId = 1);
        public DepthChart GetFullDepthChart(int? sportId = 1, int? TeamId = 1);
    }
}
