using Microsoft.AspNetCore.Http;

namespace ProvaAdmissionalCSharpApisul.UI.Web.Application.Elevador.Interfaces
{
	public interface IHsElevadorApp
	{
		void Excluir();
		void Upload(List<IFormFile> files);
	}
}