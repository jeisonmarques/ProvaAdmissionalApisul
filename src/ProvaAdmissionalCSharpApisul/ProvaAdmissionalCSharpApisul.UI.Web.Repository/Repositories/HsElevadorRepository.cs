using Microsoft.Extensions.Configuration;
using ProvaAdmissionalCSharpApisul.UI.Web.Infra.Data.Helpers;
using ProvaAdmissionalCSharpApisul.UI.Web.Service.Entities.Elevador;
using ProvaAdmissionalCSharpApisul.UI.Web.Service.Entities.Repositories.Interfaces;
using RestSharp;

namespace ProvaAdmissionalCSharpApisul.UI.Web.Infra.Data.Repositories
{
	public sealed class HsElevadorRepository : IHsElevadorRepository
	{
		private readonly string _hostWebApi;

		public HsElevadorRepository(IConfiguration configuration)
		{
			_hostWebApi = configuration.GetSection("WebApis:UrlApi").Value;
		}

		public void Excluir()
		{
			try
			{
				var servico = new RestClient(_hostWebApi);

				var pedido = new RestRequest($"/api/HsElevador/ExcluirTodosRegistros", Method.Get)
				{
					RequestFormat = DataFormat.Json,
					Timeout = -1
				};

				servico.Execute(pedido).Validar();
			}
			catch
			{
				throw;
			}
		}

		public void Incluir(IEnumerable<HsElevadorEntity> hsElevadores)
		{
			try
			{
				var servico = new RestClient(_hostWebApi);

				var pedido = new RestRequest($"/api/HsElevador/IncluirHistoricoElevadores", Method.Post)
				{
					RequestFormat = DataFormat.Json,
					Timeout = -1
				};

				pedido.AddJsonBody(hsElevadores);

				var returnApi = servico.Execute(pedido).Validar();
			}
			catch
			{
				throw;
			}
		}
	}
}