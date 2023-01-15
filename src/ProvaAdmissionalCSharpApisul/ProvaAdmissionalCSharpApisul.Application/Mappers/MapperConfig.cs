using AutoMapper;
using ProvaAdmissionalCSharpApisul.Application.HsElevador.ViewModels;
using ProvaAdmissionalCSharpApisul.Domain.Core.Entities.Elevador;

namespace ProvaAdmissionalCSharpApisul.Application.Mappers
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
            cfg.CreateMap<HsElevadorViewModel, HsElevadorEntity>().ReverseMap();
        }
    }
}