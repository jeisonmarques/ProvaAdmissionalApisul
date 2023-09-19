using Application.Reports.ElevatorUsage.Contracts.Repository;
using Application.Reports.ElevatorUsage.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Repositories;

internal class ElevatorUseDataRepository : IElevatorUsageDataRepository
{
    private static List<ElevatorUse> _elevatorUses;
    private readonly ILogger<ElevatorUseDataRepository> _logger;
    private string _inputPath;

    public ElevatorUseDataRepository(
        IConfiguration configuration,
        ILogger<ElevatorUseDataRepository> logger)
    {
        _inputPath = configuration["ElevatorUsageDataSettings:InputPath"];
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<ElevatorUse>> GetElevatorUsageDataAsync(CancellationToken cancellationToken = default)
    {
        if (_elevatorUses is null)
            await LoadData(cancellationToken);

        return _elevatorUses!.AsReadOnly();
    }

    private async Task LoadData(CancellationToken cancellationToken = default)
    {
        try
        {
            _elevatorUses = await JsonSerializer.DeserializeAsync<List<ElevatorUse>>(
                Assembly.GetEntryAssembly().GetManifestResourceStream(_inputPath),
                options: new JsonSerializerOptions { Converters = { new JsonStringEnumConverter( JsonNamingPolicy.CamelCase) } },
                cancellationToken: cancellationToken);
        }
        catch(Exception exc)
        {
            _logger.LogError(exc, "Error while attempting to load elevator usage data from data source.");
        }
    }
}
