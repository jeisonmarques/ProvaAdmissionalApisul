namespace ProvaAdmissionalCSharpApisul.Application.HsElevador.ViewModels
{
	public sealed class HsElevadorViewModel
	{
		public int Id { get; set; }
		public int Andar { get; set; }
		public char Elevador { get; set; }
		public char Turno { get; set; }
		public DateTime DtCadastro { get; set; }
	}
}