using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PollProject.Controllers;
using PollProject.Models.DTOS;
using PollProject.Repository;
using Xunit;

namespace PollProject.Tests
{
    public class PollControllerTest
    {
        private readonly PollController _pollController;

        public PollControllerTest()
        {
            var ILogger = new Mock<ILogger<PollController>>();
            ILogger<PollController> logger = ILogger.Object;

            var iPollRepository = new Mock<IPollRepository>().Object;
            var iPollStatsRepository = new Mock<IPollStatsRepository>().Object;
            var iPollOptionsRepository = new Mock<IPollOptionsRepository>().Object;

            _pollController = new PollController(logger, iPollRepository, iPollStatsRepository, iPollOptionsRepository);
        }

        [Fact]
        public async Task GetAsync_NotFoundAsync()
        {
            var actualResult = await _pollController.GetAsync(-10);
            actualResult.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task CreateAsync_NotFoundAsync()
        {
            PollDto pollDto = null;

            var actualResult = await _pollController.CreateAsync(pollDto);
            actualResult.Should().BeOfType<NotFoundResult>();
        }


        [Fact]
        public async Task VoteAsync_NotFoundAsync()
        {
            var actualResult = await _pollController.VoteAsync(-10);
            actualResult.Should().BeOfType<NotFoundResult>();
        }
    }
}
