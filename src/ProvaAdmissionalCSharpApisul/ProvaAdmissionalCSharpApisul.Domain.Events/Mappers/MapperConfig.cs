using AutoMapper;
using ProvaAdmissionalCSharpApisul.Domain.Core.Entities.Elevador;
using ProvaAdmissionalCSharpApisul.Domain.Events.Elevador.Commands;

namespace ProvaAdmissionalCSharpApisul.Domain.Events.Mappers
{
	public class MapperConfig
	{
		public static IMapper RegisterMappers()
		{
			var config = new MapperConfiguration(cfg =>
			{
				HsElevador(cfg);
			});

			return config.CreateMapper();
		}

		private static void HsElevador(IMapperConfigurationExpression cfg)
		{
			cfg.CreateMap<IncluirHsElevadorCommand, HsElevadorEntity>().AfterMap((src, dest) =>
			{
				dest.DtCadastro = DateTime.Now;
			});
		}
	}
}