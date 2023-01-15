using AutoMapper;
using MediatR;
using ProvaAdmissionalCSharpApisul.Domain.Core.Entities.Elevador;
using ProvaAdmissionalCSharpApisul.Domain.Core.Repositories;
using ProvaAdmissionalCSharpApisul.Domain.Core.Repositories.HsElevador;
using ProvaAdmissionalCSharpApisul.Domain.Events.Elevador.Commands;
using ProvaAdmissionalCSharpApisul.Domain.Events.Mappers;
using ProvaAdmissionalCSharpApisul.Infra.Data.Contexts;

namespace ProvaAdmissionalCSharpApisul.Domain.Events.Elevador.Events
{
	public sealed class IncluirListaHsElevadorEvent : RequestHandler<IncluirHsElevadorCommand>
	{
		private readonly IMapper _mapper = MapperConfig.RegisterMappers();
		private readonly IHsElevadorCommandRepository _hsElevadorCommandRepository;
		private readonly IUnitOfWork<HsElevadorContext> _unitOfWork;

		public IncluirListaHsElevadorEvent
		(
			IHsElevadorCommandRepository hsElevadorCommandRepository,
			IUnitOfWork<HsElevadorContext> unitOfWork
		)
		{
			_hsElevadorCommandRepository = hsElevadorCommandRepository;
			_unitOfWork = unitOfWork;
		}

		protected override void Handle(IncluirHsElevadorCommand request)
		{
			var lstHsElevadores = _mapper.Map<IEnumerable<HsElevadorEntity>>(request.HsElevadorersCmd);
			_hsElevadorCommandRepository.Add(lstHsElevadores);
			_unitOfWork.Commit();
		}
	}
}