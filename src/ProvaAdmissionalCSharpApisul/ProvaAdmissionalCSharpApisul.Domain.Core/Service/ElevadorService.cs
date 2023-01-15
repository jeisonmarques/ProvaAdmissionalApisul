using ProvaAdmissionalCSharpApisul.Domain.Core.Repositories.HsElevador;
using ProvaAdmissionalCSharpApisul.Domain.Core.Service.Interfaces;

namespace ProvaAdmissionalCSharpApisul.Domain.Core.Service
{
	public sealed class ElevadorService : IElevadorService
	{
		private readonly IHsElevadorQueryRepository _hsElevadorQueryRepository;

		public ElevadorService(IHsElevadorQueryRepository hsElevadorQueryRepository)
		{
			_hsElevadorQueryRepository = hsElevadorQueryRepository;
		}

		public List<int> AndarMenosUtilizado()
		{
			var lstAndares = _hsElevadorQueryRepository.List()
													   .GroupBy(p => p.Andar)
													   .Select(p => new { Andar = p.Key, Qtde = p.Count() })
													   .ToList();

			int menorQtde = lstAndares.Min(p => p.Qtde);

			return lstAndares.Where(p => p.Qtde.Equals(menorQtde))
							 .Select(p => p.Andar)
							 .OrderBy(p => p)
							 .ToList();
		}

		public List<char> ElevadorMaisFrequentado()
		{
			var lstHsElevadores = _hsElevadorQueryRepository.List()
															 .GroupBy(p => p.Elevador)
															 .OrderByDescending(p => p.Count())
															 .Select(p => new { Elevador = p.Key, Qtde = p.Count() })
															 .ToList();

			int maiorQtde = lstHsElevadores.Max(p => p.Qtde);

			return lstHsElevadores.Where(p => p.Qtde.Equals(maiorQtde))
								  .Select(p => p.Elevador)
								  .ToList();
		}

		public List<char> ElevadorMenosFrequentado()
		{
			var lstHsElevadores = _hsElevadorQueryRepository.List()
															 .GroupBy(p => p.Elevador)
															 .OrderBy(p => p.Count())
															 .Select(p => new { Elevador = p.Key, Qtde = p.Count() })
															 .ToList();

			int menorQtde = lstHsElevadores.Min(p => p.Qtde);

			return lstHsElevadores.Where(p => p.Qtde.Equals(menorQtde))
								  .Select(p => p.Elevador)
								  .ToList();
		}

		public float PercentualDeUsoElevadorA()
		{
			return CalcularPercentualUsoElevador('A');
		}

		public float PercentualDeUsoElevadorB()
		{
			return CalcularPercentualUsoElevador('B');
		}

		public float PercentualDeUsoElevadorC()
		{
			return CalcularPercentualUsoElevador('C');
		}

		public float PercentualDeUsoElevadorD()
		{
			return CalcularPercentualUsoElevador('D');
		}

		public float PercentualDeUsoElevadorE()
		{
			return CalcularPercentualUsoElevador('E');
		}

		private float CalcularPercentualUsoElevador(char elevador)
		{
			var lstHsHistorico = _hsElevadorQueryRepository.List();

			int total = lstHsHistorico.Count();
			int totalUsoElevador = lstHsHistorico.Where(p => p.Elevador.Equals(elevador)).Count();
			float pct = totalUsoElevador / (float)total * 100;

			return pct;
		}

		public List<char> PeriodoMaiorFluxoElevadorMaisFrequentado()
		{
			List<char> lstPeriodoMaiorFluxo = new();

			IEnumerable<char> lstElevadores = ElevadorMaisFrequentado();

			foreach (var elevador in lstElevadores)
			{
				char periodo = _hsElevadorQueryRepository.List()
														 .Where(p => p.Elevador.Equals(elevador))
														 .GroupBy(p => new { p.Elevador, p.Turno })
														 .OrderByDescending(p => p.Count())
														 .Select(p => p.Key.Turno)
														 .FirstOrDefault();

				lstPeriodoMaiorFluxo.Add(periodo);
			}


			return lstPeriodoMaiorFluxo;
		}

		public List<char> PeriodoMaiorUtilizacaoConjuntoElevadores()
		{
			return _hsElevadorQueryRepository.List()
											 .GroupBy(p => new { p.Turno })
											 .OrderByDescending(p => p.Count())
											 .Select(p => p.Key.Turno)
											 .ToList();
		}

		public List<char> PeriodoMenorFluxoElevadorMenosFrequentado()
		{
			List<char> lstPeriodoMenorFluxo = new();

			IEnumerable<char> lstElevadores = ElevadorMenosFrequentado();

			foreach (var elevador in lstElevadores)
			{
				char periodo = _hsElevadorQueryRepository.List()
														 .Where(p => p.Elevador.Equals(elevador))
														 .GroupBy(p => new { p.Elevador, p.Turno })
														 .OrderBy(p => p.Count())
														 .Select(p => p.Key.Turno)
														 .FirstOrDefault();

				lstPeriodoMenorFluxo.Add(periodo);
			}


			return lstPeriodoMenorFluxo;
		}
	}
}