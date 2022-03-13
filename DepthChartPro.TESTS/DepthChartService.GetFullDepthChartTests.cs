using DepthChartPro.BL.Services;
using DepthChartPro.DAL.Interfaces.Repository;
using DepthChartPro.DAL.Models;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DepthChartPro.TESTS
{
    public class DepthChartServiceGetFullDepthChartTests
    {
        private Mock<IDepthChartRepository> _mockDepthChartRepository;
        public DepthChartServiceGetFullDepthChartTests()
        {
            _mockDepthChartRepository = new Mock<IDepthChartRepository>();
        }

        [Fact]
        public async Task When_get_full_depth_chart_Async()
        {
            var depthChart = new DepthChart
            {
                PositionDepthQueueList = new List<PositionDepthQueue>
                {
                    new PositionDepthQueue
                    {
                        Position = new Position { Id=1, Code ="QB"},
                        Player = new Player { Number = 12 },
                        PositionDepth = 0
                    },
                    new PositionDepthQueue
                    {
                        Position = new Position { Id=1, Code ="QB"},
                        Player = new Player { Number = 11 },
                        PositionDepth = 1
                    },
                    new PositionDepthQueue
                    {
                        Position = new Position { Id=1, Code ="QB"},
                        Player = new Player { Number = 2 },
                        PositionDepth = 2
                    },
                    new PositionDepthQueue
                    {
                        Position = new Position { Id=2, Code ="LWR"},
                        Player = new Player { Number = 13 },
                        PositionDepth = 0
                    },
                    new PositionDepthQueue
                    {
                        Position = new Position { Id=2, Code ="LWR"},
                        Player = new Player { Number = 1 },
                        PositionDepth = 1
                    },
                    new PositionDepthQueue
                    {
                        Position = new Position { Id=2, Code ="LWR"},
                        Player = new Player { Number = 10 },
                        PositionDepth = 2
                    }
                }
            };
            _mockDepthChartRepository.Setup(dc => dc.GetFullDepthChart()).ReturnsAsync(depthChart);
            var sut = new DepthChartService(_mockDepthChartRepository.Object);
            var fullDepthChart = await sut.GetFullDepthChart();
            Assert.Equal(6, fullDepthChart.PositionDepthQueueList.Count);

            Assert.Equal(3, fullDepthChart.PositionDepthQueueList.Count(dc => dc.Position.Code == "QB"));
            Assert.Equal(12, fullDepthChart.PositionDepthQueueList[0].Player.Number);
            Assert.Equal(11, fullDepthChart.PositionDepthQueueList[1].Player.Number);
            Assert.Equal(2, fullDepthChart.PositionDepthQueueList[2].Player.Number);

            Assert.Equal(3, fullDepthChart.PositionDepthQueueList.Count(dc => dc.Position.Code == "LWR"));
            Assert.Equal(13, fullDepthChart.PositionDepthQueueList[3].Player.Number);
            Assert.Equal(1, fullDepthChart.PositionDepthQueueList[4].Player.Number);
            Assert.Equal(10, fullDepthChart.PositionDepthQueueList[5].Player.Number);

        }
    }
}
