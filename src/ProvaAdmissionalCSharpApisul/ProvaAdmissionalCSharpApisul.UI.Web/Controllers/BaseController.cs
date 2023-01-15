using Microsoft.AspNetCore.Mvc;

namespace ProvaAdmissionalCSharpApisul.UI.Web.Controllers
{
    public class BaseController : Controller
    {
        public BaseController() { }

        public enum TipoMensagem
        {
            Sucesso,
            Erro,
            Alerta,
            Informacao
        }

        public void ExibirMensagem(string mensagem, TipoMensagem tipoMensagem)
        {
            if (TempData is null)
                return;

            if (!string.IsNullOrEmpty(mensagem))
            {
                switch (tipoMensagem)
                {
                    case TipoMensagem.Sucesso:

                        TempData.Remove("Sucesso");
                        TempData.Add("Sucesso", mensagem);
                        break;

                    case TipoMensagem.Erro:

                        TempData.Remove("Erro");
                        TempData.Add("Erro", mensagem);
                        break;

                    case TipoMensagem.Alerta:

                        TempData.Remove("Alerta");
                        TempData.Add("Alerta", mensagem);
                        break;

                    case TipoMensagem.Informacao:

                        TempData.Remove("Informacao");
                        TempData.Add("Informacao", mensagem);
                        break;
                }
            }
        }
    }
}
