using ProvaAdmissionalCSharpApisul.Application.HsElevador.Service;
using ProvaAdmissionalCSharpApisul.Application.HsElevador.Service.Interfaces;
using ProvaAdmissionalCSharpApisul.Domain.Core.Repositories;
using ProvaAdmissionalCSharpApisul.Infra.Data.Contexts;
using ProvaAdmissionalCSharpApisul.Infra.Data.Repositories.Curriculum.Commands;
using ProvaAdmissionalCSharpApisul.Infra.Data.Repositories.Curriculum.Queryes;
using Microsoft.Extensions.DependencyInjection;
using ProvaAdmissionalCSharpApisul.Domain.Core.Repositories.HsElevador;
using ProvaAdmissionalCSharpApisul.Domain.Core.Service.Interfaces;
using ProvaAdmissionalCSharpApisul.Domain.Core.Service;

namespace ProvaAdmissionalCSharpApisul.Data.Infra.IoC.Modulos
{
	public static class HSElevador
	{
		public static void Initializer(IServiceCollection services)
		{
			AddApplication(services);
			AddServices(services);
			AddRepositories(services);
			AddContexts(services);
		}

		private static void AddApplication(IServiceCollection services)
		{
			services.AddScoped<IHsElevadorApp, HsElevadorApp>();
			services.AddScoped<IEstatisticaApp, EstatisticaApp>();
		}

		private static void AddServices(IServiceCollection services)
		{
			services.AddScoped<IElevadorService, ElevadorService>();
		}

		private static void AddRepositories(IServiceCollection services)
		{
			services.AddScoped<IHsElevadorCommandRepository, HsElevadorCommandRepository>();
			services.AddScoped<IHsElevadorQueryRepository, HsElevadorQueryRepository>();
		}

		private static void AddContexts(IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork<HsElevadorContext>, UnitOfWork<HsElevadorContext>>();
		}
	}
}