using System.Threading.Tasks;

namespace ScheduleTest.Infrastructure
{
    internal delegate Task ActionAsync();

    internal delegate Task ActionAsync<in T>(T parameter);
}
