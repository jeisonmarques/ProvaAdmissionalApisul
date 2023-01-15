using MediatR;

namespace ProvaAdmissionalCSharpApisul.Domain.Events.Elevador.Commands
{
	public sealed class IncluirHsElevadorCommand : IRequest
	{
		public List<IncluirHsElevadorCommand> HsElevadorersCmd { get; set; } = new();

		public int Andar { get; private set; }
		public char Elevador { get; private set; }
		public char Turno { get; private set; }

		public void AdicionarRegistro(int andar, char elevador, char turno)
		{
			Andar = andar;
			Elevador = elevador;
			Turno = turno;
		}
	}
}