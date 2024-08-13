using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IT_Desarrollo_Back.Migrations
{
    public partial class refactor_campo_pais_add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "pais",
                table: "tbl_usuarios",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pais",
                table: "tbl_usuarios");
        }
    }
}
