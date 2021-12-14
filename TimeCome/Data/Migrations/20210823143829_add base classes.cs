using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeCome.Data.Migrations
{
    public partial class addbaseclasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hashtags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hashtags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Priorities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MyTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    About = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PriorityId = table.Column<int>(type: "int", nullable: false),
                    MyTaskId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyTasks_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MyTasks_MyTasks_MyTaskId",
                        column: x => x.MyTaskId,
                        principalTable: "MyTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MyTasks_Priorities_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HashtagMyTask",
                columns: table => new
                {
                    HashCodesId = table.Column<int>(type: "int", nullable: false),
                    MyTasksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HashtagMyTask", x => new { x.HashCodesId, x.MyTasksId });
                    table.ForeignKey(
                        name: "FK_HashtagMyTask_Hashtags_HashCodesId",
                        column: x => x.HashCodesId,
                        principalTable: "Hashtags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HashtagMyTask_MyTasks_MyTasksId",
                        column: x => x.MyTasksId,
                        principalTable: "MyTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MyTaskTaskFile",
                columns: table => new
                {
                    MyTasksId = table.Column<int>(type: "int", nullable: false),
                    TaskFilesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyTaskTaskFile", x => new { x.MyTasksId, x.TaskFilesId });
                    table.ForeignKey(
                        name: "FK_MyTaskTaskFile_MyTasks_MyTasksId",
                        column: x => x.MyTasksId,
                        principalTable: "MyTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MyTaskTaskFile_TaskFiles_TaskFilesId",
                        column: x => x.TaskFilesId,
                        principalTable: "TaskFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HashtagMyTask_MyTasksId",
                table: "HashtagMyTask",
                column: "MyTasksId");

            migrationBuilder.CreateIndex(
                name: "IX_MyTasks_ApplicationUserId",
                table: "MyTasks",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MyTasks_MyTaskId",
                table: "MyTasks",
                column: "MyTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_MyTasks_PriorityId",
                table: "MyTasks",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_MyTaskTaskFile_TaskFilesId",
                table: "MyTaskTaskFile",
                column: "TaskFilesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HashtagMyTask");

            migrationBuilder.DropTable(
                name: "MyTaskTaskFile");

            migrationBuilder.DropTable(
                name: "Hashtags");

            migrationBuilder.DropTable(
                name: "MyTasks");

            migrationBuilder.DropTable(
                name: "TaskFiles");

            migrationBuilder.DropTable(
                name: "Priorities");
        }
    }
}
