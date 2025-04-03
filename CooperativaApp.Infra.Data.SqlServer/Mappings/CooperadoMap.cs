using CooperativaApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CooperativaApp.Infra.Data.SqlServer.Mappings
{
    public class CooperadoMap : IEntityTypeConfiguration<Cooperado>
    {
        public void Configure(EntityTypeBuilder<Cooperado> builder)
        {
            builder.ToTable("COOPERADOS");

            builder.HasKey(x => x.CodigoCooperadoId);

            builder.Property(x => x.CodigoCooperadoId)
                .HasColumnName("CODIGO")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Nome)
                .HasColumnName("NOMECOOPERADO")
                .HasColumnType("VARCHAR(150)")
                .IsRequired();

            builder.Property(x => x.Banco)
                .HasColumnName("BANCO")
                .HasColumnType("VARCHAR(3)");

            builder.Property(x => x.Agencia)
                .HasColumnName("AGENCIA")
                .HasColumnType("VARCHAR(5)");

            builder.Property(x => x.Conta)
                .HasColumnName("CONTA")
                .HasColumnType("VARCHAR(7)");

            builder.Property(x => x.DigitoVerificador)
                .HasColumnName("DIGITO_VERIFICADOR")
                .HasColumnType("VARCHAR(2)");

            builder.Property(x => x.Ativo)
                .HasColumnName("ATIVO")
                .IsRequired();

            builder.HasMany(x => x.ContatosFavoritos)
                .WithOne(x => x.Cooperado) 
                .HasForeignKey(x => x.CodigoCooperadoId);

            builder.HasOne(x => x.Cooperativa)
                .WithMany(x => x.Cooperados)
                .HasForeignKey(x => x.CodigoCooperativaId);
        }
    }
}