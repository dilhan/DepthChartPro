using DepthChartPro.DAL.DataAccess;
using DepthChartPro.DAL.Interfaces;
using DepthChartPro.DAL.Models;
using System.Linq;

namespace DepthChartPro.DAL.Services
{
    public class SeedingService : ISeedingService
    {
        private readonly DepthChartContext _context;
        public SeedingService(DepthChartContext context)
        {
            _context = context;
            LoadSeedData();
        }
        private void LoadSeedData()
        {
            // Seed Players
            var TomBrady = new Player { Number = 12, Name = "Tom Brady" };
            _context.Players.Add(TomBrady);
            var BlaineGabbert = new Player { Number = 11, Name = "Blaine Gabbert" };
            _context.Players.Add(BlaineGabbert);
            var KyleTrask = new Player { Number = 2, Name = "Kyle Trask" };
            _context.Players.Add(KyleTrask);

            var MikeEvans = new Player { Number = 13, Name = "Mike Evans" };
            _context.Players.Add(MikeEvans);
            var JaelonDarden = new Player { Number = 1, Name = "Jaelon Darden" };
            _context.Players.Add(JaelonDarden);
            var ScottMiller = new Player { Number = 10, Name = "Scott Miller" };
            _context.Players.Add(ScottMiller);

            // Seed Positions
            var qb = new Position { Code = "QB" };
            _context.Positions.Add(qb);

            var lwr = new Position { Code = "LWR" };
            _context.Positions.Add(lwr);

            // Seed Sports
            var nfl = new Sport { Name = "NFL" };
            _context.Sports.Add(nfl);

            // Seed Teams
            var tbb = new Team { Name = "Tampa Bay Buccaneers" };
            _context.Teams.Add(tbb);

            // Seed PositionDepthQueues
            var psdQB0 = new PositionDepthQueue { Position = qb, Player = TomBrady, PositionDepth = 0 };
            _context.PositionDepthQueues.Add(psdQB0);
            var psdQB1 = new PositionDepthQueue { Position = qb, Player = BlaineGabbert, PositionDepth = 1 };
            _context.PositionDepthQueues.Add(psdQB1);
            var psdQB2 = new PositionDepthQueue { Position = qb, Player = KyleTrask, PositionDepth = 2 };
            _context.PositionDepthQueues.Add(psdQB2);

            var psdLWR0 = new PositionDepthQueue { Position = lwr, Player = MikeEvans, PositionDepth = 0 };
            _context.PositionDepthQueues.Add(psdLWR0);
            var psdLWR1 = new PositionDepthQueue { Position = lwr, Player = JaelonDarden, PositionDepth = 1 };
            _context.PositionDepthQueues.Add(psdLWR1);
            var psdLWR2 = new PositionDepthQueue { Position = lwr, Player = ScottMiller, PositionDepth = 2 };
            _context.PositionDepthQueues.Add(psdLWR2);

            // Seed DepthCharts
            var depthChart = new DepthChart { Sport = nfl, Team = tbb };
            depthChart.PositionDepthQueueList.AddRange(_context.PositionDepthQueues.Local.ToList());
            _context.DepthChart.Add(depthChart);
            _context.SaveChanges();
        }
    }
}
