using Microsoft.EntityFrameworkCore.Migrations;

namespace scheduleBackend.Migrations
{
    public partial class Update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchedueleTable_AspNetUsers_UserId",
                table: "SchedueleTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SchedueleTable",
                table: "SchedueleTable");

            migrationBuilder.RenameTable(
                name: "SchedueleTable",
                newName: "schedueles");

            migrationBuilder.RenameIndex(
                name: "IX_SchedueleTable_UserId",
                table: "schedueles",
                newName: "IX_schedueles_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_schedueles",
                table: "schedueles",
                column: "Key");

            migrationBuilder.AddForeignKey(
                name: "FK_schedueles_AspNetUsers_UserId",
                table: "schedueles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_schedueles_AspNetUsers_UserId",
                table: "schedueles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_schedueles",
                table: "schedueles");

            migrationBuilder.RenameTable(
                name: "schedueles",
                newName: "SchedueleTable");

            migrationBuilder.RenameIndex(
                name: "IX_schedueles_UserId",
                table: "SchedueleTable",
                newName: "IX_SchedueleTable_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchedueleTable",
                table: "SchedueleTable",
                column: "Key");

            migrationBuilder.AddForeignKey(
                name: "FK_SchedueleTable_AspNetUsers_UserId",
                table: "SchedueleTable",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
