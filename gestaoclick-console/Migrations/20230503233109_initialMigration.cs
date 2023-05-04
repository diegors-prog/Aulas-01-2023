using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gestaoclick_console.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbSetFornecedor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Documento = table.Column<string>(type: "TEXT", nullable: false),
                    TipoFornecedor = table.Column<int>(type: "INTEGER", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbSetFornecedor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DbSetEndereco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FornecedorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Logradouro = table.Column<string>(type: "TEXT", nullable: false),
                    Numero = table.Column<string>(type: "TEXT", nullable: false),
                    Complemento = table.Column<string>(type: "TEXT", nullable: false),
                    Cep = table.Column<string>(type: "TEXT", nullable: false),
                    Bairro = table.Column<string>(type: "TEXT", nullable: false),
                    Cidade = table.Column<string>(type: "TEXT", nullable: false),
                    Estado = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbSetEndereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbSetEndereco_DbSetFornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "DbSetFornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DbSetProduto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FornecedorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    Imagem = table.Column<string>(type: "TEXT", nullable: false),
                    Valor = table.Column<decimal>(type: "TEXT", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbSetProduto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbSetProduto_DbSetFornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "DbSetFornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbSetEndereco_FornecedorId",
                table: "DbSetEndereco",
                column: "FornecedorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DbSetProduto_FornecedorId",
                table: "DbSetProduto",
                column: "FornecedorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbSetEndereco");

            migrationBuilder.DropTable(
                name: "DbSetProduto");

            migrationBuilder.DropTable(
                name: "DbSetFornecedor");
        }
    }
}
