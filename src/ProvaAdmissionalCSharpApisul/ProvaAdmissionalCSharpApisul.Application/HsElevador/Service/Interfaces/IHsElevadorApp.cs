using ProvaAdmissionalCSharpApisul.Application.HsElevador.ViewModels;

namespace ProvaAdmissionalCSharpApisul.Application.HsElevador.Service.Interfaces
{
	public interface IHsElevadorApp
	{
		Task Add(IEnumerable<HsElevadorViewModel> hsElevadores);
		void ExcluirTodosRegistros();
	}
}