using Application.Reports.ElevatorUsage.Contracts.Service;
using Application.Reports.ElevatorUsage.Model;
using Humanizer;
using Microsoft.Extensions.Hosting;
using static System.Console;

namespace Presentation.Console
{
    internal class ConsoleBasedElevatorUsageReportingService : IHostedService
    {
        private readonly IElevadorService _elevatorService;

        public ConsoleBasedElevatorUsageReportingService(IElevadorService elevatorService) 
        {
            _elevatorService = elevatorService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            WriteLine("Andar(es) menos utilizado(s): {0}º", string.Join("º,", _elevatorService.AndarMenosUtilizado()));
            WriteLine("Elevador(es) mais frequentado(s): {0}", string.Join(",", _elevatorService.ElevadorMaisFrequentado()));
            WriteLine("Elevador(es) menos frequentado(s): {0}", string.Join(",", _elevatorService.ElevadorMenosFrequentado()));
            WriteLine("Períodos com maior fluxo dos elevadores mais frequentados: {0}", string.Join(",", _elevatorService.PeriodoMaiorFluxoElevadorMaisFrequentado().Select(pr => ((Periods)pr).Humanize())));
            WriteLine("Períodos com menor fluxo dos elevadores menos frequentados: {0}", string.Join(",", _elevatorService.PeriodoMenorFluxoElevadorMenosFrequentado().Select(pr => ((Periods)pr).Humanize())));
            WriteLine("Período de maior utilização de todos os elevadores: {0}", string.Join(",", _elevatorService.PeriodoMaiorUtilizacaoConjuntoElevadores().Select(pr => ((Periods)pr).Humanize())));
            WriteLine("Percentual de uso do elevador A: {0}%", _elevatorService.PercentualDeUsoElevadorA());
            WriteLine("Percentual de uso do elevador B: {0}%", _elevatorService.PercentualDeUsoElevadorB());
            WriteLine("Percentual de uso do elevador C: {0}%", _elevatorService.PercentualDeUsoElevadorC());
            WriteLine("Percentual de uso do elevador D: {0}%", _elevatorService.PercentualDeUsoElevadorD());
            WriteLine("Percentual de uso do elevador E: {0}%", _elevatorService.PercentualDeUsoElevadorE());

            WriteLine();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
