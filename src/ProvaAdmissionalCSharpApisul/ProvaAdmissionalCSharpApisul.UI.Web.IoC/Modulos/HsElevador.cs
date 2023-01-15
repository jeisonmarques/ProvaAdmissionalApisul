using Microsoft.Extensions.DependencyInjection;
using ProvaAdmissionalCSharpApisul.UI.Web.Application.Elevador;
using ProvaAdmissionalCSharpApisul.UI.Web.Application.Elevador.Interfaces;
using ProvaAdmissionalCSharpApisul.UI.Web.Infra.Data.Repositories;
using ProvaAdmissionalCSharpApisul.UI.Web.Service.Entities.Repositories.Interfaces;

namespace ProvaAdmissionalCSharpApisul.UI.Web.Infra.IoC.Modulos
{
	public static class HSElevador
	{
		public static void Initializer(IServiceCollection services)
		{
			AddApplication(services);
			AddServices(services);
			AddRepositories(services);
		}

		private static void AddApplication(IServiceCollection services)
		{
			services.AddScoped<IHsElevadorApp, HsElevadorApp>();
			services.AddScoped<IEstatisticaApp, EstatisticaApp>();
		}

		private static void AddServices(IServiceCollection services)
		{

		}

		private static void AddRepositories(IServiceCollection services)
		{
			services.AddScoped<IHsElevadorRepository, HsElevadorRepository>();
			services.AddScoped<IEstatisticaRepository, EstatisticaRepository>();
		}
	}
}