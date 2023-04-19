using System.Text.RegularExpressions;

namespace SolucaoMagnoLomardo.Services;

public class ManagerService
{
    private bool canCloseProgram = false;
    private ElevatorService _service;
    
    private const int TotalAndares = 15;
    private const int TotalElevadores = 5;
    private const int ChoicesMaxLength = 1;

    public ManagerService()
    {
        _service = new ElevatorService(TotalAndares, TotalElevadores);
    }


    public bool CanCloseProgram()
    {
        return canCloseProgram;
    }

    private void ShowMainMenu()
    {
        Console.WriteLine("\nPor favor, escolha a opção desejada:" +
                          "\n1: Qual é o andar menos utilizado pelos usuários;" +
                          "\n2: Qual é o elevador mais frequentado e o período que se encontra maior fluxo;" +
                          "\n3: Qual é o elevador menos frequentado e o período que se encontra menor fluxo;" +
                          "\n4: Qual o período de maior utilização do conjunto de elevadores;" +
                          "\n5: Qual o percentual de uso de cada elevador com relação a todos os serviços prestados;" +
                          "\n6: Inserir/Atualizar os dados a serem processados;" +
                          "\n0: Sair (encerrar esta aplicação);");

    }

    private void FindLeastUsedFloor()
    {
        
    }

    private void FindMostUsedElevatorAndItsMostUsedShift()
    {
        
    }

    private void FindLeastUsedElevatorAndItsLeastUsedShift()
    {
        
    }

    private void FindShiftWithMostElevatorUsage()
    {
        
    }

    private void FindElevatorPercentUsageFromBroadUsage()
    {
        
    }
    
    private void UpdateDataToBeProcessed()
    {
        var path = GetInputFilePath();
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
            default:
                Console.WriteLine("\nOpção inválida! Tente novamente.");
                break;
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
        Console.WriteLine("The input provided is not valid. Please try again.");
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