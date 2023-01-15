using ProvaAdmissionalCSharpApisul.Domain.Core.Entities.Elevador;
using ProvaAdmissionalCSharpApisul.Domain.Core.Repositories.HsElevador;
using ProvaAdmissionalCSharpApisul.Infra.Data.Contexts;

namespace ProvaAdmissionalCSharpApisul.Infra.Data.Repositories.Curriculum.Queryes
{
	public sealed class HsElevadorQueryRepository : RepositoryQueryBase<HsElevadorEntity, HsElevadorContext>, IHsElevadorQueryRepository
	{
		public HsElevadorQueryRepository(HsElevadorContext context) : base(context)
		{
		}

		public IEnumerable<char> ListarElevadores()
		{
			return _context.HsElevador
							.Select(p => p.Elevador)
							.Distinct()
							.ToList();
		}

		public bool TemRegistros()
		{
			return _context.HsElevador
						   .Any();
		}
	}
}