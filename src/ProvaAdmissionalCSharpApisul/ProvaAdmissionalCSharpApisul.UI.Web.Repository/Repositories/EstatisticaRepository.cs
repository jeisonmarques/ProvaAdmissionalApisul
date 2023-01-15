using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProvaAdmissionalCSharpApisul.UI.Web.Infra.Data.Helpers;
using ProvaAdmissionalCSharpApisul.UI.Web.Service.Entities.Elevador;
using ProvaAdmissionalCSharpApisul.UI.Web.Service.Entities.Repositories.Interfaces;
using RestSharp;

namespace ProvaAdmissionalCSharpApisul.UI.Web.Infra.Data.Repositories
{
	public sealed class EstatisticaRepository : IEstatisticaRepository
	{
		private readonly string _hostWebApi;

		public EstatisticaRepository(IConfiguration configuration)
		{
			_hostWebApi = configuration.GetSection("WebApis:UrlApi").Value;
		}

		public EstatisticaEntity Obter()
		{
			try
			{
				var servico = new RestClient(_hostWebApi);

				var pedido = new RestRequest($"/api/Estatistica/Obter/", Method.Get)
				{
					RequestFormat = DataFormat.Json,
					Timeout = -1
				};

				var returnApi = servico.Execute(pedido).Validar();

				var oCandidate = JsonConvert.DeserializeObject<EstatisticaEntity>(returnApi.Content);
				return oCandidate;
			}
			catch
			{
				throw;
			}
		}
	}
}
