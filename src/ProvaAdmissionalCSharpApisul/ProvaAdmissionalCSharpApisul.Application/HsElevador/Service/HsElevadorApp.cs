using ProvaAdmissionalCSharpApisul.Application.HsElevador.Service.Interfaces;
using ProvaAdmissionalCSharpApisul.Application.HsElevador.ViewModels;
using MediatR;
using ProvaAdmissionalCSharpApisul.Domain.Events.Elevador.Commands;

namespace ProvaAdmissionalCSharpApisul.Application.HsElevador.Service
{
	public sealed class HsElevadorApp : IHsElevadorApp
	{
		private readonly IMediator _mediator;

		public HsElevadorApp
		(
			IMediator mediator
		)
		{
			_mediator = mediator;
		}

		public Task Add(IEnumerable<HsElevadorViewModel> hsElevadores)
		{
			IncluirHsElevadorCommand oCmd = new();

			hsElevadores.ToList()
						.ForEach(p =>
						{
							var cmd = new IncluirHsElevadorCommand();
							cmd.AdicionarRegistro(p.Andar, p.Elevador, p.Turno);
							oCmd.HsElevadorersCmd.Add(cmd);
						});

			return _mediator.Send(oCmd);
		}

		public void ExcluirTodosRegistros()
		{
			_mediator.Send(new ExcluirTodosRegistrosCommand());
		}
	}
}