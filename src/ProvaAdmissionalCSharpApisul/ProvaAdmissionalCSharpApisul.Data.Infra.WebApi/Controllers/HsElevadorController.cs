using Microsoft.AspNetCore.Mvc;
using ProvaAdmissionalCSharpApisul.Application.HsElevador.Service.Interfaces;
using ProvaAdmissionalCSharpApisul.Application.HsElevador.ViewModels;

namespace ProvaAdmissionalCSharpApisul.Data.Infra.WebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class HsElevadorController : ControllerBase
	{
		private readonly IHsElevadorApp _hsElevadorApp;

		public HsElevadorController(IHsElevadorApp elevadorApp)
		{
			_hsElevadorApp = elevadorApp;
		}


		[HttpPost]
		[Route("IncluirHistoricoElevadores")]
		public async Task<IActionResult> IncluirHistoricoElevadores([FromBody] IEnumerable<HsElevadorViewModel> hsElevadores)
		{
			try
			{
				await _hsElevadorApp.Add(hsElevadores);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}

		[HttpGet]
		[Route("ExcluirTodosRegistros")]
		public IActionResult ExcluirTodosRegistros()
		{
			try
			{
				_hsElevadorApp.ExcluirTodosRegistros();
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}