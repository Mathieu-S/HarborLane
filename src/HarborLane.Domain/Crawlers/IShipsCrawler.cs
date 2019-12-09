using System.Collections.Generic;
using System.Threading.Tasks;
using HarborLane.Domain.Models;

namespace HarborLane.Domain.Crawlers
{
    public interface IShipsCrawler
    {
        Task<IEnumerable<Ship>> GetAllShipsAsync();
    }
}