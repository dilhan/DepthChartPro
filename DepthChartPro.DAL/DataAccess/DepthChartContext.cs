using DepthChartPro.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DepthChartPro.DAL.DataAccess
{
    public class DepthChartContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<DepthChart> DepthChart { get; set; }
        public DbSet<PositionDepthQueue> PositionDepthQueues { get; set; }
        public DepthChartContext(DbContextOptions options) : base(options) 
        {
            //LoadSeedData();
        }

        private void LoadSeedData()
        {
            // Seed Players
            var TomBrady = new Player { Number = 12, Name = "Tom Brady" };
            Players.Add(TomBrady);
            var BlaineGabbert = new Player { Number = 11, Name = "Blaine Gabbert" };
            Players.Add(BlaineGabbert);
            var KyleTrask = new Player { Number = 2, Name = "Kyle Trask" };
            Players.Add(KyleTrask);

            var MikeEvans = new Player { Number = 13, Name = "Mike Evans" };
            Players.Add(MikeEvans);
            var JaelonDarden = new Player { Number = 1, Name = "Jaelon Darden" };
            Players.Add(JaelonDarden);
            var ScottMiller = new Player { Number = 10, Name = "Scott Miller" };
            Players.Add(ScottMiller);

            // Seed Positions
            var qb = new Position { Code = "QB" };
            Positions.Add(qb);

            var lwr = new Position { Code = "LWR" };
            Positions.Add(lwr);

            // Seed Sports
            var nfl = new Sport { Name = "NFL" };
            Sports.Add(nfl);

            // Seed Teams
            var tbb = new Team { Name = "Tampa Bay Buccaneers" };
            Teams.Add(tbb);

            // Seed PositionDepthQueues
            var psdQB0 = new PositionDepthQueue { Position = qb, Player = TomBrady, PositionDepth = 0 };
            PositionDepthQueues.Add(psdQB0);
            var psdQB1 = new PositionDepthQueue { Position = qb, Player = BlaineGabbert, PositionDepth = 1 };
            PositionDepthQueues.Add(psdQB1);
            var psdQB2 = new PositionDepthQueue { Position = qb, Player = KyleTrask, PositionDepth = 2 };
            PositionDepthQueues.Add(psdQB2);

            var psdLWR0 = new PositionDepthQueue { Position = lwr, Player = MikeEvans, PositionDepth = 0 };
            PositionDepthQueues.Add(psdLWR0);
            var psdLWR1 = new PositionDepthQueue { Position = lwr, Player = JaelonDarden, PositionDepth = 1 };
            PositionDepthQueues.Add(psdLWR1);
            var psdLWR2 = new PositionDepthQueue { Position = lwr, Player = ScottMiller, PositionDepth = 2 };
            PositionDepthQueues.Add(psdLWR2);

            // Seed DepthCharts
            var depthChart = new DepthChart { Sport = nfl, Team = tbb};
            depthChart.PositionDepthQueueList.AddRange(PositionDepthQueues.Local.ToList());
            DepthChart.Add(depthChart);

        }
    }
}
