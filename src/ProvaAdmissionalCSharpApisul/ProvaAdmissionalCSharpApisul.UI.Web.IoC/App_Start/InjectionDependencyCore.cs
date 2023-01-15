using Microsoft.Extensions.DependencyInjection;
using ProvaAdmissionalCSharpApisul.UI.Web.Infra.IoC.Modulos;

namespace ProvaAdmissionalCSharpApisul.UI.Web.Infra.IoC.App_Start
{
	public static class InjectionDependencyCore
	{
		public static void ConfigureServices(IServiceCollection services)
		{
			HSElevador.Initializer(services);
		}
	}
}