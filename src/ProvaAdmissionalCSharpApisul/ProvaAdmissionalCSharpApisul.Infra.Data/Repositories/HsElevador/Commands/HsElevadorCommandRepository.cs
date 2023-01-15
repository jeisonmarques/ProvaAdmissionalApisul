using ProvaAdmissionalCSharpApisul.Infra.Data.Contexts;
using ProvaAdmissionalCSharpApisul.Domain.Core.Entities.Elevador;
using ProvaAdmissionalCSharpApisul.Domain.Core.Repositories.HsElevador;
using Microsoft.EntityFrameworkCore;

namespace ProvaAdmissionalCSharpApisul.Infra.Data.Repositories.Curriculum.Commands
{
	public sealed class HsElevadorCommandRepository : RepositoryCommandBase<HsElevadorEntity, HsElevadorContext>, IHsElevadorCommandRepository
	{
		public HsElevadorCommandRepository(HsElevadorContext context) : base(context)
		{
		}

		public void ExcluirTodosRegistros()
		{
			_context.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.TB_HS_ELEVADOR");
		}
	}
}