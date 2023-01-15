using ProvaAdmissionalCSharpApisul.Domain.Core.Entities.Elevador;

namespace ProvaAdmissionalCSharpApisul.Domain.Core.Repositories.HsElevador
{
	public interface IHsElevadorQueryRepository : IRepositoryQueryBase<HsElevadorEntity>
	{
		IEnumerable<char> ListarElevadores();
		bool TemRegistros();
	}
}