using DepthChartPro.BL.Services;
using DepthChartPro.DAL.Interfaces.Repository;
using DepthChartPro.DAL.Models;
using Moq;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;
using System;

namespace DepthChartPro.TESTS
{
    public class DepthChartServiceAddTests
    {
        private Mock<IDepthChartRepository> _mockDepthChartRepository;
        public DepthChartServiceAddTests()
        {
            _mockDepthChartRepository = new Mock<IDepthChartRepository>();
        }

        [Fact]
        public async Task When_add_player_with_null_posion_Async()
        {
            _mockDepthChartRepository.Setup(dc => dc.GetFullDepthChart()).ReturnsAsync(new DepthChart());
            var sut = new DepthChartService(_mockDepthChartRepository.Object);
            var exception = await Record.ExceptionAsync(async () => await sut.AddPlayerToDepthChart(null, 12, 0));

            _mockDepthChartRepository.Verify(dc => dc.AddPlayerToDepthChart("QB", 12, 0), Times.Never);
            Assert.Equal(typeof(ArgumentNullException), exception.GetType());
            Assert.Equal("Parameter is null or empty (Parameter 'position')", exception.Message);
        }

        [Fact]
        public async Task When_add_player_to_given_posion_depth_Async()
        {
            _mockDepthChartRepository.Setup(dc => dc.GetFullDepthChart()).ReturnsAsync(new DepthChart());
            var sut = new DepthChartService(_mockDepthChartRepository.Object);
            await sut.AddPlayerToDepthChart("QB", 12, 0);

            _mockDepthChartRepository.Verify(dc => dc.AddPlayerToDepthChart("QB", 12, 0), Times.Once);
        }

        [Fact]
        public async Task When_add_player_to_existing_depth_chart_with_given_posion_depth_Async()
        {

            var depthChart = new DepthChart
            {
                PositionDepthQueueList = new List<PositionDepthQueue>
                {
                    new PositionDepthQueue
                    {
                        Position = new Position { Code ="QB"},
                        Player = new Player { Number = 12 },
                        PositionDepth = 0
                    }
                }
            };

            _mockDepthChartRepository.Setup(dc => dc.GetFullDepthChart()).ReturnsAsync(depthChart);
            var sut = new DepthChartService(_mockDepthChartRepository.Object);
            await sut.AddPlayerToDepthChart("QB", 11, 1);

            _mockDepthChartRepository.Verify(dc => dc.AddPlayerToDepthChart("QB", 11, 1), Times.Once);
        }

        [Fact]
        public async Task When_add_player_to_existing_depth_chart_without_posion_depth_should_add_to_last_Async()
        {
            var depthChart = new DepthChart
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

            _mockDepthChartRepository.Setup(dc => dc.GetFullDepthChart()).ReturnsAsync(depthChart);
            var sut = new DepthChartService(_mockDepthChartRepository.Object);
            await sut.AddPlayerToDepthChart("QB", 2, null);

            _mockDepthChartRepository.Verify(dc => dc.AddPlayerToDepthChart("QB", 2, 2), Times.Once);
        }

        [Fact]
        public async Task When_add_player_to_existing_depth_chart_on_top_should_move_others_down_Async()
        {
            var depthChart = new DepthChart
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

            _mockDepthChartRepository.Setup(dc => dc.GetFullDepthChart()).ReturnsAsync(depthChart);
            var sut = new DepthChartService(_mockDepthChartRepository.Object);
            await sut.AddPlayerToDepthChart("QB", 2, 0);

            _mockDepthChartRepository.Verify(dc => dc.AddPlayerToDepthChart("QB", 2, 0), Times.Once);

            Assert.Equal("QB", depthChart.PositionDepthQueueList[0].Position.Code);
            Assert.Equal(12, depthChart.PositionDepthQueueList[0].Player.Number);
            Assert.Equal(1, depthChart.PositionDepthQueueList[0].PositionDepth);

            Assert.Equal("QB", depthChart.PositionDepthQueueList[1].Position.Code);
            Assert.Equal(11, depthChart.PositionDepthQueueList[1].Player.Number);
            Assert.Equal(2, depthChart.PositionDepthQueueList[1].PositionDepth);

        }
    }
}
