using Application.Reports.ElevatorUsage;
using Application.Reports.ElevatorUsage.Contracts.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.TryAddSingleton<IElevadorService, ElevatorUseService>();

        return services;
    }
}
