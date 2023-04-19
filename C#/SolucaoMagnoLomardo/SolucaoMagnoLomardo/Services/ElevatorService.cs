using ProvaAdmissionalCSharpApisul;
using SolucaoMagnoLomardo.Domain;

namespace SolucaoMagnoLomardo.Services;

public class ElevatorService : IElevadorService
{
    private List<ElevatorFormInput> _inputs;
    private bool _updatingData;
    private FloorUsageStruct[] _floorUsage;
    private ElevatorUsageStruct[] _elevatorUsage;
    private ShiftUsageStruct[] _shiftsUsage;
    

    public ElevatorService(int totalFloors, int totalElevators)
    {
        _inputs = new List<ElevatorFormInput>();
        _updatingData = false;
        _floorUsage = new FloorUsageStruct[totalFloors];
        _elevatorUsage = new ElevatorUsageStruct[totalElevators];
        _shiftsUsage = new ShiftUsageStruct[Enum.GetValues<ShiftEnum>().Length];
    }

    private bool CanUpdateData()
    {
        return !_updatingData;
    }

    private void ToggleUpdatingDataStatus()
    {
        _updatingData = !_updatingData;
    }

    private void SetUpdatingDataStatus(bool value)
    {
        _updatingData = value;
    }

    public bool UpdateInputData(List<ElevatorFormInput> newData)
    {
        if (!CanUpdateData())
        {
            Console.Error.Write("Não pode autalizar os dados no momento, operação já em andamento!");
            return false;
        }

        ToggleUpdatingDataStatus();
        _inputs = newData;
        ToggleUpdatingDataStatus();
        Console.Write("Dados atualizados com sucesso!");
        return true;
    }

    public List<int> andarMenosUtilizado()
    {
        throw new NotImplementedException();
    }

    public List<char> elevadorMaisFrequentado()
    {
        throw new NotImplementedException();
    }

    public List<char> periodoMaiorFluxoElevadorMaisFrequentado()
    {
        throw new NotImplementedException();
    }

    public List<char> elevadorMenosFrequentado()
    {
        throw new NotImplementedException();
    }

    public List<char> periodoMenorFluxoElevadorMenosFrequentado()
    {
        throw new NotImplementedException();
    }

    public List<char> periodoMaiorUtilizacaoConjuntoElevadores()
    {
        throw new NotImplementedException();
    }

    public float percentualDeUsoElevadorA()
    {
        throw new NotImplementedException();
    }

    public float percentualDeUsoElevadorB()
    {
        throw new NotImplementedException();
    }

    public float percentualDeUsoElevadorC()
    {
        throw new NotImplementedException();
    }

    public float percentualDeUsoElevadorD()
    {
        throw new NotImplementedException();
    }

    public float percentualDeUsoElevadorE()
    {
        throw new NotImplementedException();
    }
}