using System;
using System.Threading.Tasks;
using HarborLane.Domain.Repositories;

namespace HarborLane.Services.Repositories
{
    public class TimeRepository : RedisClient, ITimeRepository
    {
        public async Task SetTimeAsync(DateTime dateTime)
        {
            try
            {
                await Db.Set("lastUpdate", dateTime);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<DateTime> GetTime()
        {
            try
            {
                return await Db.Get<DateTime>("lastUpdate");
            }
            catch (NullReferenceException)
            {
                return new DateTime(2000, 01, 01);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
