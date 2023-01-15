using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProvaAdmissionalCSharpApisul.Domain.Core.Entities.Elevador;

namespace ProvaAdmissionalCSharpApisul.Infra.Data.Mappings.HsElevador
{
	internal sealed class HsElevadorMapping : IEntityTypeConfiguration<HsElevadorEntity>
	{
		public void Configure(EntityTypeBuilder<HsElevadorEntity> builder)
		{
			builder.ToTable("TB_HS_ELEVADOR");

			builder.HasKey(p => p.Id);

			builder.Property(p => p.Id)
				   .HasColumnName("ID_HS_ELEVADOR")
				   .HasColumnType("INT")
				   .UseIdentityColumn(1, 1)
				   .IsRequired();

			builder.Property(p => p.Elevador)
				   .HasColumnName("NM_ELEVADOR")
				   .HasColumnType("CHAR")
				   .HasMaxLength(1)
				   .IsRequired();

			builder.Property(p => p.Andar)
				   .HasColumnName("NR_ANDAR")
				   .HasColumnType("INT")
				   .IsRequired();

			builder.Property(p => p.Turno)
				   .HasColumnName("DS_TURNO")
				   .HasColumnType("CHAR")
				   .HasMaxLength(1)
				   .IsRequired();

			builder.Property(p => p.DtCadastro)
				   .HasColumnName("DT_CADASTRO")
				   .HasColumnType("DATETIME");
		}
	}
}
