using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeetleX.Redis;
using HarborLane.Domain.Models;
using HarborLane.Domain.Repositories;

namespace HarborLane.Services.Repositories
{
    public class ShipRepository : RedisClient, IShipRepository
    {
        private readonly RedisHashTable _table;

        public ShipRepository()
        {
            _table = Db.CreateHashTable("ships");
        }

        public async Task<Ship> GetShipAsync(string key)
        {
            try
            {
                return await _table.Get<Ship>(key);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public async Task<IEnumerable<Ship>> GetShipsAsync()
        {
            var keys = await _table.Keys();
            var ships = new List<Ship>();
            
            foreach (var key in keys)
            {
                ships.Add(await GetShipAsync(key));
            }
            
            return ships;
        }

        public async Task SetShipAsync(Ship ship)
        {
            try
            {
                await _table.Set(ship.Id, ship);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public async Task SetShipsAsync(IEnumerable<Ship> ships)
        {
            foreach (var ship in ships)
            {
                await SetShipAsync(ship);
            }
        }
    }
}
