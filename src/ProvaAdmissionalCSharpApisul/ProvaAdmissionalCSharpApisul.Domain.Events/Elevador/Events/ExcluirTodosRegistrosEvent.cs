using MediatR;
using ProvaAdmissionalCSharpApisul.Domain.Core.Repositories.HsElevador;
using ProvaAdmissionalCSharpApisul.Domain.Events.Elevador.Commands;

namespace ProvaAdmissionalCSharpApisul.Domain.Events.Elevador.Events
{
	public sealed class ExcluirTodosRegistrosEvent : RequestHandler<ExcluirTodosRegistrosCommand>
	{
		private readonly IHsElevadorCommandRepository _hsElevadorCommandRepository;

		public ExcluirTodosRegistrosEvent
		(
			IHsElevadorCommandRepository hsElevadorCommandRepository
		)
		{
			_hsElevadorCommandRepository = hsElevadorCommandRepository;
		}

		protected override void Handle(ExcluirTodosRegistrosCommand request)
		{
			_hsElevadorCommandRepository.ExcluirTodosRegistros();
		}
	}
}