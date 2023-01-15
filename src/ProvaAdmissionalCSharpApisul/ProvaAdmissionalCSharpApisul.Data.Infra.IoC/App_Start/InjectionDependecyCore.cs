using ProvaAdmissionalCSharpApisul.Data.Infra.IoC.Modulos;
using Microsoft.Extensions.DependencyInjection;

namespace ProvaAdmissionalCSharpApisul.Data.Infra.IoC.App_Start
{
    public static class InjectionDependencyCore
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            HSElevador.Initializer(services);
        }        
    }
}