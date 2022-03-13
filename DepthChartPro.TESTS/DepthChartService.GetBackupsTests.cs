using DepthChartPro.BL.Services;
using DepthChartPro.DAL.Interfaces.Repository;
using DepthChartPro.DAL.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DepthChartPro.TESTS
{
    public class DepthChartServiceGetBackupsTests
    {
        private Mock<IDepthChartRepository> _mockDepthChartRepository;
        public DepthChartServiceGetBackupsTests()
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
                    },
                    new PositionDepthQueue
                    {
                        Position = new Position { Code ="QB"},
                        Player = new Player { Number = 2 },
                        PositionDepth = 2
                    }
                }
            };
        }

        [Fact]
        public async Task When_get_backups_for_first_palyer_Async()
        {
            var depthChart = setupDepthChart();
            _mockDepthChartRepository.Setup(dc => dc.GetFullDepthChart()).ReturnsAsync(depthChart);
            var sut = new DepthChartService(_mockDepthChartRepository.Object);
            var backups = await sut.GetBackups("QB", 12);
            Assert.Equal(2, backups.Count());
            Assert.Equal(11, backups.ToList()[0].Number);
            Assert.Equal(2, backups.ToList()[1].Number);
        }

        [Fact]
        public async Task When_get_backups_for_second_palyer_Async()
        {
            var depthChart = setupDepthChart();
            _mockDepthChartRepository.Setup(dc => dc.GetFullDepthChart()).ReturnsAsync(depthChart);
            var sut = new DepthChartService(_mockDepthChartRepository.Object);
            var backups = await sut.GetBackups("QB", 11);
            Assert.Equal(1, backups.Count());
            Assert.Equal(2, backups.ToList()[0].Number);
        }

        [Fact]
        public async Task When_get_backups_for_last_palyer_Async()
        {
            var depthChart = setupDepthChart();
            _mockDepthChartRepository.Setup(dc => dc.GetFullDepthChart()).ReturnsAsync(depthChart);
            var sut = new DepthChartService(_mockDepthChartRepository.Object);
            var backups = await sut.GetBackups("QB", 2);
            Assert.Empty(backups);
        }

        [Fact]
        public async Task When_get_backups_for_non_existing_palyer_Async()
        {
            var depthChart = setupDepthChart();
            _mockDepthChartRepository.Setup(dc => dc.GetFullDepthChart()).ReturnsAsync(depthChart);
            var sut = new DepthChartService(_mockDepthChartRepository.Object);
            var backups = await sut.GetBackups("QB", 1);
            Assert.Empty(backups);
        }
    }
}
