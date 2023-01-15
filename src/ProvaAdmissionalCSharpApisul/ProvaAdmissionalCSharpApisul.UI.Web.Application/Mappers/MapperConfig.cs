using AutoMapper;
using ProvaAdmissionalCSharpApisul.UI.Web.Application.Elevador.ViewModels;
using ProvaAdmissionalCSharpApisul.UI.Web.Service.Entities.Elevador;

namespace ProvaAdmissionalCSharpApisul.UI.Web.Application.Elevador.Mappers
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
            cfg.CreateMap<EstatisticaEntity, EstatisticaViewModel>().ReverseMap();
        }
    }
}