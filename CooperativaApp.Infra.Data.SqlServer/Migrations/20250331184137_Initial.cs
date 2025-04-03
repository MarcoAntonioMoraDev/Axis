using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CooperativaApp.Infra.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COOPERATIVAS",
                columns: table => new
                {
                    CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMECOOPERATIVA = table.Column<string>(type: "VARCHAR(150)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COOPERATIVAS", x => x.CODIGO);
                });

            migrationBuilder.CreateTable(
                name: "COOPERADOS",
                columns: table => new
                {
                    CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMECOOPERADO = table.Column<string>(type: "VARCHAR(150)", nullable: false),
                    BANCO = table.Column<string>(type: "VARCHAR(3)", nullable: true),
                    AGENCIA = table.Column<string>(type: "VARCHAR(5)", nullable: true),
                    CONTA = table.Column<string>(type: "VARCHAR(7)", nullable: true),
                    DIGITO_VERIFICADOR = table.Column<string>(type: "VARCHAR(2)", nullable: true),
                    ATIVO = table.Column<bool>(type: "bit", nullable: false),
                    CodigoCooperativaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COOPERADOS", x => x.CODIGO);
                    table.ForeignKey(
                        name: "FK_COOPERADOS_COOPERATIVAS_CodigoCooperativaId",
                        column: x => x.CodigoCooperativaId,
                        principalTable: "COOPERATIVAS",
                        principalColumn: "CODIGO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CONTATOS_FAVORITOS",
                columns: table => new
                {
                    CODIGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "VARCHAR(150)", nullable: false),
                    TIPO_CHAVE_PIX = table.Column<int>(type: "INT", nullable: false),
                    CHAVE_PIX = table.Column<string>(type: "VARCHAR(150)", nullable: false),
                    COOPERADO_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONTATOS_FAVORITOS", x => x.CODIGO);
                    table.ForeignKey(
                        name: "FK_CONTATOS_FAVORITOS_COOPERADOS_COOPERADO_ID",
                        column: x => x.COOPERADO_ID,
                        principalTable: "COOPERADOS",
                        principalColumn: "CODIGO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CONTATOS_FAVORITOS_COOPERADO_ID",
                table: "CONTATOS_FAVORITOS",
                column: "COOPERADO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_COOPERADOS_CodigoCooperativaId",
                table: "COOPERADOS",
                column: "CodigoCooperativaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CONTATOS_FAVORITOS");

            migrationBuilder.DropTable(
                name: "COOPERADOS");

            migrationBuilder.DropTable(
                name: "COOPERATIVAS");
        }
    }
}
