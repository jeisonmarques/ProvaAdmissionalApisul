namespace ProvaAdmissionalCSharpApisul.Domain.Core.Entities.Elevador
{
	public sealed class HsElevadorEntity : BaseEntity
	{
		public int Andar { get; set; }
		public char Elevador { get; set; }
		public char Turno { get; set; }
		public DateTime DtCadastro { get; set; }
	}
}