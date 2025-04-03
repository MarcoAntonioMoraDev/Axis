using CooperativaApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CooperativaApp.Infra.Data.SqlServer.Mappings
{
    public class ContatoFavoritoMap : IEntityTypeConfiguration<ContatoFavorito>
    {
        public void Configure(EntityTypeBuilder<ContatoFavorito> builder)
        {
            builder.ToTable("CONTATOS_FAVORITOS");

            builder.HasKey(cf => cf.CodigoContatoFavoritoId);

            builder.Property(cf => cf.CodigoContatoFavoritoId)
                .HasColumnName("CODIGO")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(cf => cf.Nome)
                .HasColumnName("NOME")
                .HasColumnType("VARCHAR(150)")
                .IsRequired();

            builder.Property(cf => cf.TipoChavePix)
                .HasColumnName("TIPO_CHAVE_PIX")
                .HasColumnType("INT")
                .IsRequired();

            builder.Property(cf => cf.ChavePix)
                .HasColumnName("CHAVE_PIX")
                .HasColumnType("VARCHAR(150)")
                .IsRequired();

            builder.Property(cf => cf.CodigoCooperadoId)
                .HasColumnName("COOPERADO_ID"); // Permite nulo

            builder.HasOne(cf => cf.Cooperado)
                .WithMany(c => c.ContatosFavoritos)
                .HasForeignKey(cf => cf.CodigoCooperadoId);
        }
    }
}