using Application.Reports.ElevatorUsage.Contracts.Repository;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.TryAddSingleton<IElevatorUsageDataRepository, ElevatorUseDataRepository>();

        return services;
    }
}
