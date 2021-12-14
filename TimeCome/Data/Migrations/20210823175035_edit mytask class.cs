using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeCome.Data.Migrations
{
    public partial class editmytaskclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyTasks_AspNetUsers_ApplicationUserId",
                table: "MyTasks");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "MyTasks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MyTasks_AspNetUsers_ApplicationUserId",
                table: "MyTasks",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyTasks_AspNetUsers_ApplicationUserId",
                table: "MyTasks");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "MyTasks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_MyTasks_AspNetUsers_ApplicationUserId",
                table: "MyTasks",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
