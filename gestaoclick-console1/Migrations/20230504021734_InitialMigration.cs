using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gestaoclick_console1.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fornecedores",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    documento = table.Column<string>(type: "varchar(14)", nullable: false),
                    tipo_fornecedor = table.Column<int>(type: "INTEGER", nullable: false),
                    ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fornecedores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "enderecos",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    fornecedor_id = table.Column<int>(type: "INTEGER", nullable: false),
                    logradouro = table.Column<string>(type: "varchar(200)", nullable: false),
                    numero = table.Column<string>(type: "varchar(50)", nullable: false),
                    complemento = table.Column<string>(type: "varchar(250)", nullable: false),
                    cep = table.Column<string>(type: "varchar(8)", nullable: false),
                    bairro = table.Column<string>(type: "varchar(100)", nullable: false),
                    cidade = table.Column<string>(type: "varchar(100)", nullable: false),
                    estado = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enderecos", x => x.id);
                    table.ForeignKey(
                        name: "FK_enderecos_fornecedores_fornecedor_id",
                        column: x => x.fornecedor_id,
                        principalTable: "fornecedores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "produtos",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    fornecedor_id = table.Column<int>(type: "INTEGER", nullable: false),
                    nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    descricao = table.Column<string>(type: "varchar(1000)", nullable: false),
                    imagem = table.Column<string>(type: "varchar(100)", nullable: false),
                    valor = table.Column<decimal>(type: "TEXT", nullable: false),
                    data_cadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produtos", x => x.id);
                    table.ForeignKey(
                        name: "FK_produtos_fornecedores_fornecedor_id",
                        column: x => x.fornecedor_id,
                        principalTable: "fornecedores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_enderecos_fornecedor_id",
                table: "enderecos",
                column: "fornecedor_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_produtos_fornecedor_id",
                table: "produtos",
                column: "fornecedor_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "enderecos");

            migrationBuilder.DropTable(
                name: "produtos");

            migrationBuilder.DropTable(
                name: "fornecedores");
        }
    }
}
