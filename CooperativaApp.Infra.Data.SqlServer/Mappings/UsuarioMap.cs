using CooperativaApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CooperativaApp.Infra.Data.SqlServer.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("USUARIOS");

            builder.HasKey(x => x.CodigoUsuarioId);

            builder.Property(x => x.CodigoUsuarioId)
                .HasColumnName("CODIGO")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Nome)
                .HasColumnName("NOMEUSUARIO")
                .HasColumnType("VARCHAR(150)")
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnName("EMAILUSUARIO")
                .HasColumnType("VARCHAR(100)")
                .IsRequired();

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Senha)
                .HasColumnName("SENHAUSUARIO")
                .HasColumnType("VARCHAR(100)")
                .IsRequired();
        }
    }
}