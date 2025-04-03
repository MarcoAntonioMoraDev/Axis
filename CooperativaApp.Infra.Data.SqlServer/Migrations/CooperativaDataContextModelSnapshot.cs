﻿// <auto-generated />
using CooperativaApp.Infra.Data.SqlServer.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CooperativaApp.Infra.Data.SqlServer.Migrations
{
    [DbContext(typeof(CooperativaDataContext))]
    partial class CooperativaDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ContatoFavorito", b =>
                {
                    b.Property<int>("CodigoContatoFavoritoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CODIGO");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodigoContatoFavoritoId"));

                    b.Property<string>("ChavePix")
                        .IsRequired()
                        .HasColumnType("VARCHAR(150)")
                        .HasColumnName("CHAVE_PIX");

                    b.Property<int>("CodigoCooperadoId")
                        .HasColumnType("int")
                        .HasColumnName("COOPERADO_ID");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(150)")
                        .HasColumnName("NOME");

                    b.Property<int>("TipoChavePix")
                        .HasColumnType("INT")
                        .HasColumnName("TIPO_CHAVE_PIX");

                    b.HasKey("CodigoContatoFavoritoId");

                    b.HasIndex("CodigoCooperadoId");

                    b.ToTable("CONTATOS_FAVORITOS", (string)null);
                });

            modelBuilder.Entity("CooperativaApp.Domain.Entities.Cooperado", b =>
                {
                    b.Property<int>("CodigoCooperadoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CODIGO");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodigoCooperadoId"));

                    b.Property<string>("Agencia")
                        .HasColumnType("VARCHAR(5)")
                        .HasColumnName("AGENCIA");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit")
                        .HasColumnName("ATIVO");

                    b.Property<string>("Banco")
                        .HasColumnType("VARCHAR(3)")
                        .HasColumnName("BANCO");

                    b.Property<int>("CodigoCooperativaId")
                        .HasColumnType("int");

                    b.Property<string>("Conta")
                        .HasColumnType("VARCHAR(7)")
                        .HasColumnName("CONTA");

                    b.Property<string>("DigitoVerificador")
                        .HasColumnType("VARCHAR(2)")
                        .HasColumnName("DIGITO_VERIFICADOR");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(150)")
                        .HasColumnName("NOMECOOPERADO");

                    b.HasKey("CodigoCooperadoId");

                    b.HasIndex("CodigoCooperativaId");

                    b.ToTable("COOPERADOS", (string)null);
                });

            modelBuilder.Entity("CooperativaApp.Domain.Entities.Cooperativa", b =>
                {
                    b.Property<int>("CodigoCooperativaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CODIGO");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodigoCooperativaId"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(150)")
                        .HasColumnName("NOMECOOPERATIVA");

                    b.HasKey("CodigoCooperativaId");

                    b.ToTable("COOPERATIVAS", (string)null);
                });

            modelBuilder.Entity("CooperativaApp.Domain.Entities.Usuario", b =>
                {
                    b.Property<int>("CodigoUsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CODIGO");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodigoUsuarioId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("EMAILUSUARIO");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(150)")
                        .HasColumnName("NOMEUSUARIO");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("SENHAUSUARIO");

                    b.HasKey("CodigoUsuarioId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("USUARIOS", (string)null);
                });

            modelBuilder.Entity("ContatoFavorito", b =>
                {
                    b.HasOne("CooperativaApp.Domain.Entities.Cooperado", "Cooperado")
                        .WithMany("ContatosFavoritos")
                        .HasForeignKey("CodigoCooperadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cooperado");
                });

            modelBuilder.Entity("CooperativaApp.Domain.Entities.Cooperado", b =>
                {
                    b.HasOne("CooperativaApp.Domain.Entities.Cooperativa", "Cooperativa")
                        .WithMany("Cooperados")
                        .HasForeignKey("CodigoCooperativaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cooperativa");
                });

            modelBuilder.Entity("CooperativaApp.Domain.Entities.Cooperado", b =>
                {
                    b.Navigation("ContatosFavoritos");
                });

            modelBuilder.Entity("CooperativaApp.Domain.Entities.Cooperativa", b =>
                {
                    b.Navigation("Cooperados");
                });
#pragma warning restore 612, 618
        }
    }
}
