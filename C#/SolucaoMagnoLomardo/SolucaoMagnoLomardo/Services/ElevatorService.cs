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

        var helper = _floorUsage.OrderBy(x => x.UsageCount).ToList();
        var minUsage = helper.First().UsageCount;
        helper = helper.Where(h => h.UsageCount == minUsage).ToList();
        var aux = helper.Select(floor => floor.FloorLevel).ToList();
        return aux.OrderBy(x => x).ToList();
    }

    public List<char> elevadorMaisFrequentado()
    {
        if (!CanProcessData() || !HasData())
        {
            throw new ApplicationException();
        }

        var helper = _elevatorUsage.OrderByDescending(x => x.UsageCount).ToList();
        var maxUsage = helper.First().UsageCount;
        helper = helper.Where(x => x.UsageCount == maxUsage).ToList();

        return helper.Select(elevator => elevator.Elevator).ToList();
    }

    public List<char> periodoMaiorFluxoElevadorMaisFrequentado()
    {
        var result = new List<char>();
        var desiredElevators = elevadorMaisFrequentado();

        var helper = _inputs.Where(x => desiredElevators.Exists(d => d == x.Elevador[0]));

        foreach (var elevator in desiredElevators)
        {
            var countM = helper.Count(entry => entry.Shift == ShiftEnum.Matutino);
            var countV = helper.Count(entry => entry.Shift == ShiftEnum.Vespertino);
            var countN = helper.Count(entry => entry.Shift == ShiftEnum.Noturno);
            
            if (countM >= countV && countM >= countN)
            {
                result.Add(ShiftUsage.ConvertShiftToChar(ShiftEnum.Matutino));
            }
            else if (countV >= countN && countV >= countM)
            {
                result.Add(ShiftUsage.ConvertShiftToChar(ShiftEnum.Vespertino));
            }
            else if (countN >= countM && countN >= countV)
            {
                result.Add(ShiftUsage.ConvertShiftToChar(ShiftEnum.Noturno));
            }
        }
        return result;
    }

    public List<char> elevadorMenosFrequentado()
    {
        if (!CanProcessData() || !HasData())
        {
            throw new ApplicationException();
        }

        var helper = _elevatorUsage.OrderBy(x => x.UsageCount).ToList();
        var minUsage = helper.First().UsageCount;
        helper = helper.Where(h => h.UsageCount == minUsage).ToList();

        return helper.Select(elevator => elevator.Elevator).ToList();
    }

    public List<char> periodoMenorFluxoElevadorMenosFrequentado()
    {
        var result = new List<char>();
        var desiredElevators = elevadorMenosFrequentado();

        var helper = _inputs.Where(x => desiredElevators.Exists(d => d == x.Elevador[0]));

        foreach (var elevator in desiredElevators)
        {
            var countM = helper.Count(entry => entry.Shift == ShiftEnum.Matutino);
            var countV = helper.Count(entry => entry.Shift == ShiftEnum.Vespertino);
            var countN = helper.Count(entry => entry.Shift == ShiftEnum.Noturno);

            if (countM != 0)
            {
                if (countV == 0 && countN == 0)
                {
                    result.Add(ShiftUsage.ConvertShiftToChar(ShiftEnum.Matutino));
                }
                else if (countV > countN && countV > countM)
                {
                    result.Add(ShiftUsage.ConvertShiftToChar(ShiftEnum.Vespertino));
                }
                else
                {
                    result.Add(countM > countN
                        ? ShiftUsage.ConvertShiftToChar(ShiftEnum.Matutino)
                        : ShiftUsage.ConvertShiftToChar(ShiftEnum.Noturno));
                }
            }
            else if (countV != 0)
            {
                if (countN == 0)
                {
                    result.Add(ShiftUsage.ConvertShiftToChar(ShiftEnum.Vespertino));
                }
                else if(countV > countN)
                {
                    result.Add(ShiftUsage.ConvertShiftToChar(ShiftEnum.Vespertino));
                }
                else
                {
                    result.Add(ShiftUsage.ConvertShiftToChar(ShiftEnum.Noturno));
                }
                
            }
            else if (countN != 0)
            {
                result.Add(countM >= countN
                    ? ShiftUsage.ConvertShiftToChar(ShiftEnum.Matutino)
                    : ShiftUsage.ConvertShiftToChar(ShiftEnum.Noturno));
            }
            
        }
        return result;
    }

    public List<char> periodoMaiorUtilizacaoConjuntoElevadores()
    {
        if (!CanProcessData() || !HasData())
        {
            throw new ApplicationException();
        }

        var helper = _shiftsUsage.OrderByDescending(shift => shift.UsageCount >= shift.UsageCount).ToList();
        var maxUsage = helper.First().UsageCount;
        helper = helper.Where(h => h.UsageCount == maxUsage).ToList();

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