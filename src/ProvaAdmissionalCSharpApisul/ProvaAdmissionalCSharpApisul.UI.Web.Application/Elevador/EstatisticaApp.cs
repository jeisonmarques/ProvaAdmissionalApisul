using AutoMapper;
using ProvaAdmissionalCSharpApisul.UI.Web.Application.Elevador.Interfaces;
using ProvaAdmissionalCSharpApisul.UI.Web.Application.Elevador.Mappers;
using ProvaAdmissionalCSharpApisul.UI.Web.Application.Elevador.ViewModels;
using ProvaAdmissionalCSharpApisul.UI.Web.Service.Entities.Repositories.Interfaces;

namespace ProvaAdmissionalCSharpApisul.UI.Web.Application.Elevador
{
	public sealed class EstatisticaApp : IEstatisticaApp
	{
		private readonly IMapper _mapper = MapperConfig.RegisterMappers();
		private readonly IEstatisticaRepository _estatisticaRepository;

		public EstatisticaApp(IEstatisticaRepository estatisticaRepository)
		{
			_estatisticaRepository = estatisticaRepository;
		}

		public EstatisticaViewModel Obter()
		{
			var oEstatisticaEntity = _estatisticaRepository.Obter();
			var oEstatistivaVM = _mapper.Map<EstatisticaViewModel>(oEstatisticaEntity);
			return oEstatistivaVM;
		}
	}
}