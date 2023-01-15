using Microsoft.AspNetCore.Mvc;
using System;
using ProvaAdmissionalCSharpApisul.UI.Web.Controllers;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using ProvaAdmissionalCSharpApisul.UI.Web.Application.Elevador.Interfaces;

namespace ProvaAdmissionalCSharpApisul.UI.Web.Areas.Elevador.Controllers
{
	[Area("Elevador")]
	[Route("Elevador/[controller]")]
	public class ElevadorController : BaseController
	{
		private readonly IHsElevadorApp _hsElevadorApp;
		private readonly IEstatisticaApp _estatisticaApp;

		public ElevadorController
		(
			IHsElevadorApp hsElevadorApp,
			IEstatisticaApp estatisticaApp
		)
		{
			_hsElevadorApp = hsElevadorApp;
			_estatisticaApp = estatisticaApp;
		}

		#region Index

		[Route("Index")]
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[Route("Upload")]
		[HttpPost]
		public IActionResult Upload(List<IFormFile> files)
		{
			try
			{
				_hsElevadorApp.Upload(files);

				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}

		[Route("ObterEstatisticas")]
		[HttpGet]
		public PartialViewResult ObterEstatisticas()
		{
			try
			{
				var oEstatistica = _estatisticaApp.Obter();
				return PartialView("_EstatisticasPartial", oEstatistica);
			}
			catch (Exception ex)
			{
				ExibirMensagem(ex.Message, TipoMensagem.Erro);
				return PartialView("_Erro");
			}
		}

		[Route("Excluir")]
		[HttpGet]
		public JsonResult Excluir()
		{
			try
			{
				_hsElevadorApp.Excluir();
				return Json(new { FlSucesso = true, Mensagem = "Todos os registros foram excluídos com sucesso!" });
			}
			catch (Exception ex)
			{
				return Json(new { FlSucesso = false, Mensagem = ex.Message });
			}
		}

		#endregion
	}
}