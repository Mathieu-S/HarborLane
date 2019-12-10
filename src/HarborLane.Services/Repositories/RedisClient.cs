using BeetleX.Redis;

namespace HarborLane.Services.Repositories
{
    public abstract class RedisClient
    {
        protected readonly RedisDB Db;

        protected RedisClient()
        {
            Db = new RedisDB { DataFormater = new JsonFormater() };
            Db.Host.AddWriteHost("localhost");
        }
    }
}
