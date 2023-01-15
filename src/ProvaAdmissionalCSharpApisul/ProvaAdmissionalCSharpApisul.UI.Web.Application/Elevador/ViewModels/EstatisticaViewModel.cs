namespace ProvaAdmissionalCSharpApisul.UI.Web.Application.Elevador.ViewModels
{
	public sealed class EstatisticaViewModel
	{
		public List<int> AndarMenosUtilizado { get; set; }

		public List<char> ElevadorMaisFrequentado { get; set; }
		public List<char> PeriodoMaiorFluxoElevadorMaisUtilizado { get; set; }

		public List<char> ElevadorMenosUtilizado { get; set; }
		public List<char> PeriodoMenorFluxoElevadorMenosUtilizado { get; set; }

		public char PeriodoMaiorUtilizacaoCjElevadores { get; set; }

		public List<Tuple<char, float>> PctUsoElevadores { get; set; } = new();
	}
}