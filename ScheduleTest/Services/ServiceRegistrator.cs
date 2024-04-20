using Microsoft.Extensions.DependencyInjection;
using ScheduleTest.Infrastructure.Extensions;
using ScheduleTest.Models;
using ScheduleTest.Services.Interfaces;

namespace ScheduleTest.Services
{
    internal static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
           .AddTransient<IDataService, DataService>()
           .AddTransient<IUserDialog, UserDialog>();
    }
}
