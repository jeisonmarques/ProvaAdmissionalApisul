using Microsoft.Extensions.Hosting;
using Application;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await Host
                    .CreateDefaultBuilder(args)
                    .ConfigureServices(services =>
                    {
                        services.AddApplicationServices()
                                .AddInfrastructureServices()
                                .AddHostedService<ConsoleBasedElevatorUsageReportingService>();
                    })
                    .Build()
                    .RunAsync();
        }
    }
}