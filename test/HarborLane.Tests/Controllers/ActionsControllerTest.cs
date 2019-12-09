using System.Collections.Generic;
using System.Linq;
using HarborLane.Api.Controllers;
using HarborLane.Domain.Crawlers;
using HarborLane.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace HarborLane.Tests.Controllers
{
    public class ActionsControllerTest
    {
        private readonly IList<Ship> _fakeShips;

        public ActionsControllerTest()
        {
            _fakeShips = new List<Ship>
            {
                new Ship {Id = "1", Name = "Fake 1"},
                new Ship {Id = "2", Name = "Fake 2"}
            };
        }

        [Fact]
        public void GetShipsList_From_Wiki()
        {
            // Arrange
            var logger = Substitute.For<ILogger<ActionsController>>();
            var shipsCrawler = Substitute.For<IShipsCrawler>();
            shipsCrawler.GetAllShipsAsync().Returns(_fakeShips);
            var controller = new ActionsController(logger, shipsCrawler);

            // Act
            var result = controller.CrawlWiki().Result;

            // Assert
            var payload = Assert.IsAssignableFrom<OkObjectResult>(result);
            var ships = Assert.IsAssignableFrom<IEnumerable<Ship>>(payload.Value);
            Assert.Equal(2, ships.Count());
        }
    }
}