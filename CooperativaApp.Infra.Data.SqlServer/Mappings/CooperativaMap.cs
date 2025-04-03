using CooperativaApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CooperativaApp.Infra.Data.SqlServer.Mappings
{
    public class CooperativaMap : IEntityTypeConfiguration<Cooperativa>
    {
        public void Configure(EntityTypeBuilder<Cooperativa> builder)
        {
            builder.ToTable("COOPERATIVAS");

            builder.Property(x => x.CodigoCooperativaId)
                .HasColumnName("CODIGO")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Nome)
                .HasColumnName("NOMECOOPERATIVA")
                .HasColumnType("VARCHAR(150)")
                .IsRequired();

            builder.Property(x => x.Ativo)
                .IsRequired();


            builder.HasMany(x => x.Cooperados)
                .WithOne(x => x.Cooperativa)
                .HasForeignKey(x => x.CodigoCooperativaId);
        }
    }
}