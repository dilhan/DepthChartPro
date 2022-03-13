using DepthChartPro.BL.Services;
using DepthChartPro.DAL.Interfaces.Repository;
using DepthChartPro.DAL.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DepthChartPro.TESTS
{
    public class DepthChartServiceRemoveTests
    {
        private Mock<IDepthChartRepository> _mockDepthChartRepository;
        public DepthChartServiceRemoveTests()
        {
            _mockDepthChartRepository = new Mock<IDepthChartRepository>();
        }

        private DepthChart setupDepthChart()
        {
            return new DepthChart
            {
                PositionDepthQueueList = new List<PositionDepthQueue>
                {
                    new PositionDepthQueue
                    {
                        Position = new Position { Code ="QB"},
                        Player = new Player { Number = 12 },
                        PositionDepth = 0
                    },
                    new PositionDepthQueue
                    {
                        Position = new Position { Code ="QB"},
                        Player = new Player { Number = 11 },
                        PositionDepth = 1
                    }
                }
            };
        }

        [Fact]
        public async Task When_remove_player_with_null_posion_Async()
        {
            _mockDepthChartRepository.Setup(dc => dc.GetFullDepthChart()).ReturnsAsync(new DepthChart());
            var sut = new DepthChartService(_mockDepthChartRepository.Object);
            var exception = await Record.ExceptionAsync(async () => await sut.AddPlayerToDepthChart(null, 12, 0));

            _mockDepthChartRepository.Verify(dc => dc.RemovePlayerFromDepthChart(It.IsAny<int>()), Times.Never);
            Assert.Equal(typeof(ArgumentNullException), exception.GetType());
            Assert.Equal("Parameter is null or empty (Parameter 'position')", exception.Message);
        }

        [Fact]
        public async Task When_remove_player_not_existsAsync()
        {
            var depthChart = setupDepthChart();
            _mockDepthChartRepository.Setup(dc => dc.GetFullDepthChart()).ReturnsAsync(depthChart);
            var sut = new DepthChartService(_mockDepthChartRepository.Object);
            var result = await sut.RemovePlayerFromDepthChart("QB", 2);

            _mockDepthChartRepository.Verify(dc => dc.RemovePlayerFromDepthChart(It.IsAny<int>()), Times.Never);

            Assert.Null(result);
        }

        [Fact]
        public async Task When_remove_existing_playerAsync()
        {
            var depthChart = setupDepthChart();
            _mockDepthChartRepository.Setup(dc => dc.GetFullDepthChart()).ReturnsAsync(depthChart);
            var sut = new DepthChartService(_mockDepthChartRepository.Object);
            var result = await sut.RemovePlayerFromDepthChart("QB", 12);

            _mockDepthChartRepository.Verify(dc => dc.RemovePlayerFromDepthChart(0), Times.Once);

            Assert.Equal(12,result.Number);
        }
    }
}
