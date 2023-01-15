using ProvaAdmissionalCSharpApisul.UI.Web.Service.Entities.Elevador;

namespace ProvaAdmissionalCSharpApisul.UI.Web.Service.Entities.Repositories.Interfaces
{
	public interface IHsElevadorRepository
	{
		void Excluir();
		void Incluir(IEnumerable<HsElevadorEntity> hsElevadores);
	}
}