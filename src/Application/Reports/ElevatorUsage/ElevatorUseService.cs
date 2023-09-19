using Application.Reports.ElevatorUsage.Contracts.Repository;
using Application.Reports.ElevatorUsage.Contracts.Service;
using Application.Reports.ElevatorUsage.Model;

namespace Application.Reports.ElevatorUsage;

internal class ElevatorUseService : IElevadorService
{
    private readonly IReadOnlyCollection<ElevatorUse> _elevatorUses;
    private static List<Periods> _periods = default!;
    private static List<Elevators> _elevators = default!;
    private static List<int> _floors = default!;
    private const int FLOOR_COUNT = 16;

    public ElevatorUseService(IElevatorUsageDataRepository repository)
    {
        _elevatorUses = repository
                            .GetElevatorUsageDataAsync()
                            .GetAwaiter()
                            .GetResult();

        _elevators = Enum.GetValues<Elevators>().ToList();
        _periods = Enum.GetValues<Periods>().ToList();
        _floors = new List<int>(FLOOR_COUNT);
        for (var i = 0; i < FLOOR_COUNT; i++)
            _floors.Add(i);
    }

    public List<int> AndarMenosUtilizado()
        => GetFloorsWithNoElevatorUse()
                .Union(_elevatorUses.GroupBy(eu => eu.Floor)
                                    .OrderBy(eu => eu.Count())
                                    .Select(eug => eug.Key))
                                    .ToList();

    public List<char> ElevadorMaisFrequentado()
        => _elevatorUses
                    .GroupBy(eu => eu.Elevator)
                    .OrderByDescending(eu => eu.Count())
                    .Select(eug => (char)eug.Key)
                    .Union(GetUnusedElevators())
                    .ToList();

    public List<char> ElevadorMenosFrequentado()
        => GetUnusedElevators()
                    .Union(_elevatorUses
                    .GroupBy(eu => eu.Elevator)
                    .OrderBy(eu => eu.Count())
                    .Select(eug => (char)eug.Key))
                    .ToList();

    public float PercentualDeUsoElevadorA()
        => PercentualDeUsoElevador(Elevators.A);

    public float PercentualDeUsoElevadorB()
        => PercentualDeUsoElevador(Elevators.B);

    public float PercentualDeUsoElevadorC()
        => PercentualDeUsoElevador(Elevators.C);

    public float PercentualDeUsoElevadorD()
        => PercentualDeUsoElevador(Elevators.D);

    public float PercentualDeUsoElevadorE()
        => PercentualDeUsoElevador(Elevators.E);

    private float PercentualDeUsoElevador(Elevators elevator)
        => (float)Math.Round((double)_elevatorUses.Count(eu => eu.Elevator == elevator) / _elevatorUses.Count * 100, 2);

    public List<char> PeriodoMaiorUtilizacaoConjuntoElevadores()
        => _elevatorUses
            .GroupBy(eu => eu.Period)
            .OrderByDescending(eu => eu.Count())
            .Select(eu => (char)eu.Key)
            .Distinct()
            .Union(GetUnusedPeriods())
            .ToList();

    public List<char> PeriodoMenorFluxoElevadorMenosFrequentado()
    {
        var leastUsedPeriods = new List<char>();
        var leastUsedElevators = ElevadorMenosFrequentado();

        leastUsedElevators.ForEach(lue => {
            _elevatorUses.GroupBy(eu => (char)eu.Period)
                         .OrderBy(eug => eug.Count())
                         .ToList()
                         .ForEach(eug => {
                             if (!leastUsedPeriods.Contains(eug.Key))
                                 leastUsedPeriods.Add(eug.Key);
                         });
        });

        return GetUnusedPeriods()
                .Union(leastUsedPeriods)
                .Distinct()
                .ToList();
    }

    public List<char> PeriodoMaiorFluxoElevadorMaisFrequentado()
    {
        var mostUsedPeriods = new List<char>();
        var mostUsedElevators = ElevadorMaisFrequentado();

        mostUsedElevators.ForEach(lue => {
            _elevatorUses.GroupBy(eu => (char)eu.Period)
                         .OrderByDescending(eug => eug.Count())
                         .ToList()
                         .ForEach(eug => {
                             if (!mostUsedPeriods.Contains(eug.Key))
                                 mostUsedPeriods.Add(eug.Key);
                         });
        });

        return mostUsedPeriods
                .Union(GetUnusedPeriods())
                .Distinct()
                .ToList();
    }

    private List<char> GetUnusedElevators()
    {
        var unusedElevators = new List<char>();

        var _uses = _elevatorUses.Select(eu => eu.Elevator).Distinct();
        _elevators.ForEach(el =>
        {
            if (!_uses.Contains(el))
                unusedElevators.Add((char)el);
        });

        return unusedElevators;
    }

    private List<char> GetUnusedPeriods()
    {
        var unusedPeriods = new List<char>();

        var _uses = _elevatorUses.Select(eu => eu.Period).Distinct();
        _periods.ForEach(pd =>
        {
            if (!_uses.Contains(pd))
                unusedPeriods.Add((char)pd);
        });

        return unusedPeriods;
    }

    private List<int> GetFloorsWithNoElevatorUse()
    {
        var floorsWithNoElevatorUse = new List<int>();

        var _uses = _elevatorUses.Select(eu => eu.Floor).Distinct();
        _floors.ForEach(fl =>
        {
            if (!_uses.Contains(fl))
                floorsWithNoElevatorUse.Add(fl);
        });

        return floorsWithNoElevatorUse;
    }
}