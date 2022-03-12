
namespace DepthChartPro.DAL.Models
{
    public class PositionDepthQueue
    {
        public int Id { get; set; }
        public Position Position { get; set; } = new Position();
        public Player Player { get; set; } = new Player();
        public int PositionDepth { get; set; }
    }
}
