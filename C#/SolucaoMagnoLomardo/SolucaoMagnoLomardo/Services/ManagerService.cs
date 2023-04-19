using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using SolucaoMagnoLomardo.Domain;
using File = System.IO.File;

namespace SolucaoMagnoLomardo.Services;

public class ManagerService
{
    private bool canCloseProgram = false;
    private ElevatorService _elevatorService;
    
    private const int TotalAndares = 16;
    private const int TotalElevadores = 5;
    private const int ChoicesMaxLength = 1;

    public ManagerService()
    {
        _elevatorService = new ElevatorService(TotalAndares, TotalElevadores);
    }


    public bool CanCloseProgram()
    {
        return canCloseProgram;
    }

    private void ShowMainMenu()
    {
        if (!_elevatorService.HasData())
        {
            Console.WriteLine("\n\nNo momento não há dados a serem processados no sistema!" +
                              "\nPor favor, escolha a opção desejada:" +
                              "\n6: Inserir/Atualizar os dados a serem processados;" +
                              "\n0: Sair (encerrar esta aplicação);");
        }
        else
        {
            Console.WriteLine("\n\nPor favor, escolha a opção desejada:" +
                              "\n1: Qual é o andar menos utilizado pelos usuários;" +
                              "\n2: Qual é o elevador mais frequentado e o período que se encontra maior fluxo;" +
                              "\n3: Qual é o elevador menos frequentado e o período que se encontra menor fluxo;" +
                              "\n4: Qual o período de maior utilização do conjunto de elevadores;" +
                              "\n5: Qual o percentual de uso de cada elevador com relação a todos os serviços prestados;" +
                              "\n6: Inserir/Atualizar os dados a serem processados;" +
                              "\n7: Limpar cache de dados;" +
                              "\n0: Sair (encerrar esta aplicação);");
        }

    }

    private void FindLeastUsedFloor()
    {
        var floors = _elevatorService.andarMenosUtilizado();
        Console.WriteLine("Os andares menos usados são: ");
        ShowIntList(floors);
        
    }

    private void FindMostUsedElevatorAndItsMostUsedShift()
    {
        var elevators = _elevatorService.elevadorMaisFrequentado();
        var shifts = _elevatorService.periodoMaiorFluxoElevadorMaisFrequentado();
        Console.WriteLine("Os turnos dos elevadores mais usados são: ");
        ShowElevatorsAndShift(elevators, shifts);
    }

    private void FindLeastUsedElevatorAndItsLeastUsedShift()
    {
        var result = _elevatorService.periodoMenorFluxoElevadorMenosFrequentado();
    }

    private void FindShiftWithMostElevatorUsage()
    {
        var res = _elevatorService.periodoMaiorUtilizacaoConjuntoElevadores();
        Console.WriteLine("Os Turnos com o maior uso dos elevadores são: ");
        ShowCharList(res);
    }

    private void FindElevatorPercentUsageFromBroadUsage()
    {
        var elevatorA = _elevatorService.percentualDeUsoElevadorA() * 100;
        var elevatorB = _elevatorService.percentualDeUsoElevadorB() * 100;
        var elevatorC = _elevatorService.percentualDeUsoElevadorC() * 100;
        var elevatorD = _elevatorService.percentualDeUsoElevadorD() * 100;
        var elevatorE = _elevatorService.percentualDeUsoElevadorE() * 100;
        
        Console.WriteLine("O percentual de uso de cada elevador é:" +
                          "\nA: {0}%, B: {1}%, C: {2}%, D: {3}%, E: {4}%", elevatorA, elevatorB, elevatorC, elevatorC, elevatorE);
    }
    
    private void UpdateDataToBeProcessed()
    {
        var path = GetInputFilePath();

        if (!ValidateFile(path))
        {
            return;
        }

        using (StreamReader r = new StreamReader(path))
        {
            var jsonString = r.ReadToEnd();
            var data = JsonConvert.DeserializeObject<ElevatorFormInput[]>(jsonString);
            _elevatorService.UpdateInputData(data.ToList());
        }

    }

    private void ClearDataCache()
    {
        _elevatorService.ClearInputData();
    }

    public void ManageLoop()
    {
        ShowMainMenu();
        var choice = GetUserInputToInt();

        switch (choice)
        {
            case 1:
                FindLeastUsedFloor();
                break;
            case 2:
                FindMostUsedElevatorAndItsMostUsedShift();
                break;
            case 3:
                FindLeastUsedElevatorAndItsLeastUsedShift();
                break;
            case 4:
                FindShiftWithMostElevatorUsage();
                break;
            case 5:
                FindElevatorPercentUsageFromBroadUsage();
                break;
            case 6:
                UpdateDataToBeProcessed();
                break;
            case 7:
                ClearDataCache();
                break;
            default:
                return;
        }
    }

    private string GetInputFilePath()
    {
        var executablePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly()?.Location);
        Console.WriteLine("Para continuar, precisamos de um caminho VÁLIDO para o arquivo de entrada." +
                          "\nPor favor, insira o caminho para o arquivo com os dados de entrada:" +
                          "\n(Diretório atual: {0})", executablePath);
        return Console.ReadLine();
    }

    public string[] GetInputFileLines()
    {
        var file = GetInputFilePath();

        if (!File.Exists(file))
        {
            Console.Error.Write("The file provided does not exist!");
            return null;
        }

        return File.ReadAllLines(file);
    }

    private void ShowInvalidInputMessage()
    {
        Console.WriteLine("A entrada fornecida não é válida! Por favor tente novamente.");
    }
    private void ShowIntList(List<int> toShow)
    {
        foreach (var entry in toShow)
        {
            Console.Write(" {0},", entry);
        }
    }

    private void ShowCharList(List<char> toShow)
    {
        foreach (var entry in toShow)
        {
            Console.Write(" {0},", entry);
        }
    }

    private void ShowElevatorsAndShift(List<char> elevators, List<char> shift)
    {
        if (elevators.Count != shift.Count)
        {
            Console.Error.WriteLine("Erro: entradas com tamanhos distintos!");
            return;
        }

        for (int i = 0; i < elevators.Count; i++)
        {
            Console.Write("{0}: {1} ", elevators[i], shift[i]);
        }
    }
    
    private int GetUserInputToInt()
    {
        var input = Console.ReadLine();

        if (!ValidateUserInput(input))
        {
            ShowInvalidInputMessage();
            return -1;
        }

        if (int.Parse(input!) == 0)
        {
            canCloseProgram = true;
            QuitApplication(0);
            return 0;
        }

        return int.Parse(input!);
    }
    
    private bool ValidateUserInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;
        
        if (input.Length > ChoicesMaxLength)
            return false;

        const string regexQuery = "^[0-9]*$";
        if (!Regex.IsMatch(input, regexQuery))
            return false;

        var inputNumber = int.Parse(input);
        if (inputNumber < 0)
            return false;

        return true;
    }

    private bool ValidateFile(string filepath)
    {
        if (!File.Exists(filepath))
        {
            Console.Error.Write("Erro: arquivo inexistente!");
            return false;
        }

        var info = new FileInfo(filepath);
        if (!info.Extension.Equals(".json"))
        {
            Console.Error.Write("Erro: O arquivo fornecido NÃO é um JSON!");
            return false;
        }

        return true;
    }
    
    
    private void QuitApplication(int exitCode)
    {
        if (!canCloseProgram)
        {
            Console.Error.WriteLine("Attempting to close the program incorrectly!");
            return;
        }
        
        Environment.Exit(exitCode);
    }
}