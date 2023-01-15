using ProvaAdmissionalCSharpApisul.Application.HsElevador.Service.Interfaces;
using ProvaAdmissionalCSharpApisul.Application.HsElevador.ViewModels;
using ProvaAdmissionalCSharpApisul.Domain.Core.Repositories.HsElevador;
using ProvaAdmissionalCSharpApisul.Domain.Core.Service.Interfaces;
using System.Reflection;

namespace ProvaAdmissionalCSharpApisul.Application.HsElevador.Service
{
	public sealed class EstatisticaApp : IEstatisticaApp
	{
		private readonly IElevadorService _elevadorService;
		private readonly IHsElevadorQueryRepository _hsElevadorQueryRepository;

		public EstatisticaApp
		(
			IElevadorService elevadorService,
			IHsElevadorQueryRepository hsElevadorQueryRepository
		)
		{
			_elevadorService = elevadorService;
			_hsElevadorQueryRepository = hsElevadorQueryRepository;
		}

		public EstatisticaViewModel Calcular()
		{
			EstatisticaViewModel oEstatistica = new();

			bool temRegistros = _hsElevadorQueryRepository.TemRegistros();

			if (temRegistros)
			{
				oEstatistica.AndarMenosUtilizado = _elevadorService.AndarMenosUtilizado();

				oEstatistica.ElevadorMaisFrequentado = _elevadorService.ElevadorMaisFrequentado();
				oEstatistica.PeriodoMaiorFluxoElevadorMaisUtilizado = _elevadorService.PeriodoMaiorFluxoElevadorMaisFrequentado();

				oEstatistica.ElevadorMenosUtilizado = _elevadorService.ElevadorMenosFrequentado();
				oEstatistica.PeriodoMenorFluxoElevadorMenosUtilizado = _elevadorService.PeriodoMenorFluxoElevadorMenosFrequentado();

				oEstatistica.PeriodoMaiorUtilizacaoCjElevadores = _elevadorService.PeriodoMaiorUtilizacaoConjuntoElevadores().FirstOrDefault();

				var elevadores = _hsElevadorQueryRepository.ListarElevadores();

				foreach (var elevador in elevadores)
				{
					float pct = (float)_elevadorService.GetType().
														GetMethod($"PercentualDeUsoElevador{elevador}").
														Invoke(_elevadorService, null);
							
					//float pct = (float)_elevadorService.GetType()
					//								   .GetMethod($"PercentualDeUsoElevador{elevador}")
					//								   .Invoke(typeof(IElevadorService), null);

					oEstatistica.PctUsoElevadores.Add(new Tuple<char, float>(elevador, pct));
				}
			}

			return oEstatistica;
		}
	}
}