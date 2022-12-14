using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TryitterAPI.Migrations
{
    public partial class RemoveStudentAssociation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Students_StudentId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_StudentId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Post");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Post_Id",
                table: "Students",
                column: "Post_Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Post_Post_Id",
                table: "Students",
                column: "Post_Id",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Post_Post_Id",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_Post_Id",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Post_StudentId",
                table: "Post",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Students_StudentId",
                table: "Post",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}