using System;
using System.Threading.Tasks;

namespace HarborLane.Domain.Repositories
{
    public interface ITimeRepository
    {
        Task SetTimeAsync(DateTime dateTime);
        Task<DateTime> GetTime();
    }
}
