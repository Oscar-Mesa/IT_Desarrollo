using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IT_Desarrollo_Back.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_preguntas",
                columns: table => new
                {
                    pkid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_preguntas", x => x.pkid);
                });

            migrationBuilder.CreateTable(
                name: "tbl_roles",
                columns: table => new
                {
                    pkid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_roles", x => x.pkid);
                });

            migrationBuilder.CreateTable(
                name: "tbl_usuarios",
                columns: table => new
                {
                    pkid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    apellido = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    codigo_pais = table.Column<int>(type: "integer", nullable: false),
                    telefono = table.Column<int>(type: "integer", nullable: false),
                    img = table.Column<byte[]>(type: "bytea", nullable: false),
                    contrasena = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    RolId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_usuarios", x => x.pkid);
                    table.ForeignKey(
                        name: "FK_tbl_usuarios_tbl_roles_RolId",
                        column: x => x.RolId,
                        principalTable: "tbl_roles",
                        principalColumn: "pkid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_respuestas",
                columns: table => new
                {
                    pkid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pregunta = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    respuesta = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    PreguntaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_respuestas", x => x.pkid);
                    table.ForeignKey(
                        name: "FK_tbl_respuestas_tbl_preguntas_PreguntaId",
                        column: x => x.PreguntaId,
                        principalTable: "tbl_preguntas",
                        principalColumn: "pkid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_respuestas_tbl_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "tbl_usuarios",
                        principalColumn: "pkid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_respuestas_PreguntaId",
                table: "tbl_respuestas",
                column: "PreguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_respuestas_UsuarioId",
                table: "tbl_respuestas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_usuarios_RolId",
                table: "tbl_usuarios",
                column: "RolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_respuestas");

            migrationBuilder.DropTable(
                name: "tbl_preguntas");

            migrationBuilder.DropTable(
                name: "tbl_usuarios");

            migrationBuilder.DropTable(
                name: "tbl_roles");
        }
    }
}
