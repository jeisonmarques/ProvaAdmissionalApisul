using Application.Reports.ElevatorUsage.Model;

namespace Application.Reports.ElevatorUsage.Contracts.Repository;

public interface IElevatorUsageDataRepository
{
    public Task<IReadOnlyCollection<ElevatorUse>> GetElevatorUsageDataAsync(CancellationToken cancellationToken = default);
}