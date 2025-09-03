using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RankList.Migrations
{
    /// <inheritdoc />
    public partial class AjusteRelacionamentoListaPessoal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstabelecimentoIds",
                table: "ListasPessoais");

            migrationBuilder.CreateTable(
                name: "ListaPessoalEstabelecimentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ListaPessoalId = table.Column<int>(type: "integer", nullable: false),
                    EstabelecimentoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaPessoalEstabelecimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListaPessoalEstabelecimentos_ListasPessoais_ListaPessoalId",
                        column: x => x.ListaPessoalId,
                        principalTable: "ListasPessoais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListaPessoalEstabelecimentos_ListaPessoalId",
                table: "ListaPessoalEstabelecimentos",
                column: "ListaPessoalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListaPessoalEstabelecimentos");

            migrationBuilder.AddColumn<List<int>>(
                name: "EstabelecimentoIds",
                table: "ListasPessoais",
                type: "integer[]",
                nullable: false);
        }
    }
}
