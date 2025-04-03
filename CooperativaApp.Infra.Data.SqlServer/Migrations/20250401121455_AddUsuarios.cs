using CooperativaApp.Domain.Helpers;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CooperativaApp.Infra.Data.SqlServer.Migrations
{
    public partial class AddUsuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USUARIOS",
                columns: table => new
                {
                    CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMEUSUARIO = table.Column<string>(type: "VARCHAR(150)", nullable: false),
                    EMAILUSUARIO = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    SENHAUSUARIO = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIOS", x => x.CODIGO);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USUARIOS_EMAILUSUARIO",
                table: "USUARIOS",
                column: "EMAILUSUARIO",
                unique: true);

            // Inserindo um usuário padrão
            migrationBuilder.InsertData(
                table: "USUARIOS",
                columns: new[] { "NOMEUSUARIO", "EMAILUSUARIO", "SENHAUSUARIO" },
                values: new object[] { "Administrador", "admin@cooperativa.com", SHA256Helper.Encrypt("SenhaSegura123") }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USUARIOS");
        }
    }
}
