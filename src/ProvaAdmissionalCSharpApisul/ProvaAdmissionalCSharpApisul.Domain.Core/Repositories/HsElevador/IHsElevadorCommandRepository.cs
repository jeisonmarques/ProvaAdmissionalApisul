using ProvaAdmissionalCSharpApisul.Domain.Core.Entities.Elevador;

namespace ProvaAdmissionalCSharpApisul.Domain.Core.Repositories.HsElevador
{
	public interface IHsElevadorCommandRepository : IRepositoryCommandBase<HsElevadorEntity>
	{
		void ExcluirTodosRegistros();
	}
}