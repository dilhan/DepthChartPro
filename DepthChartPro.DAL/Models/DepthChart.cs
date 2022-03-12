
using System.Collections.Generic;

namespace DepthChartPro.DAL.Models
{
    public class DepthChart
    {
        public int Id { get; set; }
        public Team Team { get; set; }
        public Sport Sport { get; set; }
        public List<PositionDepthQueue> PositionDepthQueueList { get; set; } = new List<PositionDepthQueue>();
    }
}
