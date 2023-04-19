// See https://aka.ms/new-console-template for more information
using SolucaoMagnoLomardo.Services;
using SolucaoMagnoLomardo.Domain;

var _managerService = new ManagerService();
Console.WriteLine("Apisul - Teste Admissional\nMagno Lomardo");

do
{
    _managerService.ManageLoop();
} while (!_managerService.CanCloseProgram());

