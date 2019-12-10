using System.Collections.Generic;
using System.Threading.Tasks;
using HarborLane.Domain.Models;

namespace HarborLane.Domain.Repositories
{
    public interface IShipRepository
    {
        Task<Ship> GetShipAsync(string key);
        Task<IEnumerable<Ship>> GetShipsAsync();
        Task SetShipAsync(Ship ship);
        Task SetShipsAsync(IEnumerable<Ship> ships);
    }
}