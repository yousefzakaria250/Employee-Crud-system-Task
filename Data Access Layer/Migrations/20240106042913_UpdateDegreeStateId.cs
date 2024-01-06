using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    public partial class UpdateDegreeStateId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_DegreeState_DegreeStateId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "DegreeStateId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_DegreeState_DegreeStateId",
                table: "AspNetUsers",
                column: "DegreeStateId",
                principalSchema: "Security",
                principalTable: "DegreeState",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_DegreeState_DegreeStateId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "DegreeStateId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_DegreeState_DegreeStateId",
                table: "AspNetUsers",
                column: "DegreeStateId",
                principalSchema: "Security",
                principalTable: "DegreeState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
