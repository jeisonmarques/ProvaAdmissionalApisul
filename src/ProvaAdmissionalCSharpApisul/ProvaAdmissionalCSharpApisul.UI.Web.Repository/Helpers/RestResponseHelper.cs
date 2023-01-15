using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace ProvaAdmissionalCSharpApisul.UI.Web.Infra.Data.Helpers
{
	public static class RestResponseHelper
	{
		public static RestResponse Validar(this RestResponse restResponse)
		{
			if (restResponse.StatusCode != HttpStatusCode.OK)
				throw new ApplicationException(restResponse.ErrorMessage ?? JsonConvert.DeserializeObject<string>(restResponse.Content));

			return restResponse;
		}
	}
}