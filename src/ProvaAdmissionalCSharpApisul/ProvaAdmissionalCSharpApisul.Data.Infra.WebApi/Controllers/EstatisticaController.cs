using Microsoft.AspNetCore.Mvc;
using ProvaAdmissionalCSharpApisul.Application.HsElevador.Service.Interfaces;

namespace ProvaAdmissionalCSharpApisul.Data.Infra.WebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EstatisticaController : Controller
	{
		private readonly IEstatisticaApp _estatisticaApp;

		public EstatisticaController(IEstatisticaApp estatisticaApp)
		{
			_estatisticaApp = estatisticaApp;
		}

		[HttpGet]
		[Route("Obter")]
		public IActionResult Obter()
		{
			try
			{
				var oEstatistica = _estatisticaApp.Calcular();
				return Ok(oEstatistica);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}
	}
}
