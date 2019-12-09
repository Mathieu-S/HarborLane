using System.Collections.Generic;
using HarborLane.Domain.Models;
using HarborLane.Services.Crawlers;
using Xunit;

namespace HarborLane.Tests.Crawlers
{
    public class ShipsCrawlerTest
    {
        [Fact]
        public async void Get_Ships_List_From_Wiki()
        {
            var shipsCrawler = new ShipsCrawler();
            var ships = await shipsCrawler.GetAllShipsAsync();

            Assert.IsAssignableFrom<IEnumerable<Ship>>(ships);
        }
    }
}