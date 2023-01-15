using Microsoft.AspNetCore.Mvc;

namespace ProvaAdmissionalCSharpApisul.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        public RedirectToActionResult Index()
        {
            return RedirectToAction("Index", "Elevador", new { area = "Elevador" });
        }
    }
}