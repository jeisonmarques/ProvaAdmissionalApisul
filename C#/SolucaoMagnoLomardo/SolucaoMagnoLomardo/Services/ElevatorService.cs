using System.Data;
using ProvaAdmissionalCSharpApisul;
using SolucaoMagnoLomardo.Domain;

namespace SolucaoMagnoLomardo.Services;

public class ElevatorService : IElevadorService
{
    private List<ElevatorFormInput> _inputs;
    private bool _hasData;
    private bool _canProccessData;
    private List<FloorUsage> _floorUsage;
    private List<ElevatorUsage> _elevatorUsage;
    private List<ShiftUsage> _shiftsUsage;
    

    public ElevatorService(int totalFloors, int totalElevators)
    {
        _inputs = new List<ElevatorFormInput>();
        _hasData = false;
        _canProccessData = false;
        _floorUsage = new List<FloorUsage>();
        _elevatorUsage = new List<ElevatorUsage>();
        _shiftsUsage = new List<ShiftUsage>();
    }

    public bool HasData()
    {
        return _hasData;
    }

    private bool CanProcessData()
    {
        if (!_canProccessData)
        {
            Console.WriteLine("No momento os dados não podem ser processados. Atualize os dados e tente novamente!");
        }
        
        return _canProccessData;
    }

    private void ToggleProcessingDataStatus()
    {
        _canProccessData = !_canProccessData;
    }

    private void SetUpdatingDataStatus(bool value)
    {
        _canProccessData = value;
    }

    public void ClearInputData()
    {
        _inputs.Clear();
        _hasData = false;
        Console.WriteLine("Dados limpos com sucesso às {0}!", DateTime.Now);
    }

    private void PreProcessInputData()
    {
        if (!HasData())
        {
            Console.Error.WriteLine("Erro: Não há dados para pré-processar!");
            SetUpdatingDataStatus(false);
            return;
        }

        foreach (var curInput in _inputs)
        {
            // Andar
            if (_floorUsage.Exists(x => x.FloorLevel == curInput.Andar))
            {
                _floorUsage.Find(x => 
                    x.FloorLevel == curInput.Andar
                )!.UsageCount++;
            }
            else
            {
                _floorUsage.Add(new FloorUsage
                {
                    FloorLevel = curInput.Andar,
                    UsageCount = 1
                });
            }
            
            // Elevador
            if (_elevatorUsage.Exists(x => x.Elevator == curInput.Elevador[0]))
            {
                _elevatorUsage.Find(x => 
                    x.Elevator == curInput.Elevador[0]
                )!.UsageCount++;
            }
            else
            {
                _elevatorUsage.Add(new ElevatorUsage
                {
                    Elevator = curInput.Elevador[0],
                    UsageCount = 1
                });
            }
            
            // Turno
            if (_shiftsUsage.Exists(x => x.Shift == curInput.Shift))
            {
                _shiftsUsage.Find(x => 
                    x.Shift == curInput.Shift
                )!.UsageCount++;
            }
            else
            {
                _shiftsUsage.Add(new ShiftUsage
                {
                    Shift = curInput.Shift,
                    UsageCount = 1
                });
            }
        }
        
        SetUpdatingDataStatus(true);
    }
    
    public bool UpdateInputData(List<ElevatorFormInput> newData)
    {
        if (HasData())
        {
            Console.Error.Write("Não pode autalizar os dados no momento, operação já em andamento! Limpe os dados e tente novamente.");
            return false;
        }

        ToggleProcessingDataStatus();
        _inputs = newData;
        ToggleProcessingDataStatus();
        _hasData = true;
        Console.Write("Dados atualizados com sucesso!");
        PreProcessInputData();
        return true;
    }

    public List<int> andarMenosUtilizado()
    {
        if (!CanProcessData() || !HasData())
        {
            throw new ApplicationException();
        }
        
        var helper = _floorUsage.Where(floorUsage => floorUsage.UsageCount <= floorUsage.UsageCount);
        var minUsage = helper.First().UsageCount;
        helper = helper.Where(h => h.UsageCount == minUsage);

        return helper.Select(floor => floor.FloorLevel).ToList();
    }

    public List<char> elevadorMaisFrequentado()
    {
        if (!CanProcessData() || !HasData())
        {
            throw new ApplicationException();
        }

        var helper = _elevatorUsage.Where(elevatorUsage => elevatorUsage.UsageCount >= elevatorUsage.UsageCount);
        var maxUsage = helper.First().UsageCount;
        helper = helper.Where(h => h.UsageCount == maxUsage);

        return helper.Select(elevator => elevator.Elevator).ToList();
    }

    public List<char> periodoMaiorFluxoElevadorMaisFrequentado()
    {
        var result = new List<char>();
        var desiredElevators = elevadorMaisFrequentado();
        return result;
    }

    public List<char> elevadorMenosFrequentado()
    {
        if (!CanProcessData() || !HasData())
        {
            throw new ApplicationException();
        }

        var helper = _elevatorUsage.Where(elevatorUsage => elevatorUsage.UsageCount <= elevatorUsage.UsageCount);
        var minUsage = helper.First().UsageCount;
        helper = helper.Where(h => h.UsageCount == minUsage);

        return helper.Select(elevator => elevator.Elevator).ToList();
    }

    public List<char> periodoMenorFluxoElevadorMenosFrequentado()
    {
        var helper = _shiftsUsage.Where(su => su.UsageCount <= su.UsageCount);
        var desiredShift = helper.First();

        var flow = _inputs.Where(ipt => ipt.Shift == desiredShift.Shift);
        var usage = new List<ElevatorUsage>();

        foreach (var input in flow) 
        {
            if(usage.Exists(x => x.Elevator == input.Elevador[0]))
            {
                usage.Find(x =>
                    x.Elevator == input.Elevador[0]
                )!.UsageCount++;
            }
            else
            {
                usage.Add(new ElevatorUsage
                {
                    Elevator = input.Elevador[0],
                    UsageCount = 1
                });
            }
        }

        var elevator = usage.Where(u => u.UsageCount <= u.UsageCount);
        var minUsage = elevator.First().UsageCount;
        elevator = elevator.Where(e => e.UsageCount == minUsage);

        return elevator.Select(e => e.Elevator).ToList();
    }

    public List<char> periodoMaiorUtilizacaoConjuntoElevadores()
    {
        if (!CanProcessData() || !HasData())
        {
            throw new ApplicationException();
        }

        var helper = _shiftsUsage.Where(shift => shift.UsageCount >= shift.UsageCount);
        var maxUsage = helper.First().UsageCount;
        helper = helper.Where(h => h.UsageCount == maxUsage);

        var result = new List<char>();

        foreach (var h in helper)
        {
            result.Add(h.ConvertShiftToChar());
        }

        return result;
    }

    public float percentualDeUsoElevadorA()
    {
        if (!CanProcessData())
        {
            return 0f;
        }

        var totalUsage = _inputs.Count;
        var elevatorAUsage = _elevatorUsage.Find(x => x.Elevator == 'A')!.UsageCount;
        var res = (float) elevatorAUsage / totalUsage;
        return (float) Math.Round(res, 2);
    }

    public float percentualDeUsoElevadorB()
    {
        if (!CanProcessData())
        {
            return 0f;
        }
        
        var totalUsage = _inputs.Count;
        var elevatorBUsage = _elevatorUsage.Find(x => x.Elevator == 'B')!.UsageCount;
        var res = (float) elevatorBUsage / totalUsage;
        return (float) Math.Round(res, 2);
    }

    public float percentualDeUsoElevadorC()
    {
        if (!CanProcessData())
        {
            return 0f;
        }
        
        var totalUsage = _inputs.Count;
        var elevatorCUsage = _elevatorUsage.Find(x => x.Elevator == 'C')!.UsageCount;
        var res = (float) elevatorCUsage / totalUsage;
        return (float) Math.Round(res, 2);
    }

    public float percentualDeUsoElevadorD()
    {
        if (!CanProcessData())
        {
            return 0f;
        }
        
        var totalUsage = _inputs.Count;
        var elevatorDUsage = _elevatorUsage.Find(x => x.Elevator == 'D')!.UsageCount;
        var res = (float) elevatorDUsage / totalUsage;
        return (float) Math.Round(res, 2);
    }

    public float percentualDeUsoElevadorE()
    {
        if (!CanProcessData())
        {
            return 0f;
        }
        
        var totalUsage = _inputs.Count;
        var elevatorEUsage = _elevatorUsage.Find(x => x.Elevator == 'E')!.UsageCount;
        var res = (float) elevatorEUsage / totalUsage;
        return (float) Math.Round(res, 2);
    }
}