using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddUserIdToBookmark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "Bookmark",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Bookmark",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_userId",
                table: "Bookmark",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_AspNetUsers_userId",
                table: "Bookmark",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_AspNetUsers_userId",
                table: "Bookmark");

            migrationBuilder.DropIndex(
                name: "IX_Bookmark_userId",
                table: "Bookmark");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Bookmark");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Bookmark");
        }
    }
}
