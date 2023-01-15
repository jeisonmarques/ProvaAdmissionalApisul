using Microsoft.EntityFrameworkCore;
using ProvaAdmissionalCSharpApisul.Domain.Core.Entities.Elevador;
using ProvaAdmissionalCSharpApisul.Infra.Data.Mappings.HsElevador;

namespace ProvaAdmissionalCSharpApisul.Infra.Data.Contexts
{
	public sealed class HsElevadorContext : DbContext
	{
		public HsElevadorContext(DbContextOptions<HsElevadorContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		#region Histórico do Elevador

		public DbSet<HsElevadorEntity> HsElevador { get; set; }

		#endregion

		#region Mapeamentos

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			ModuloHsElevador(modelBuilder);
		}

		private static void ModuloHsElevador(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new HsElevadorMapping());
		}

		#endregion
	}
}