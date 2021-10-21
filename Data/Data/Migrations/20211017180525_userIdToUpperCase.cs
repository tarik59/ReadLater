using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class userIdToUpperCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_AspNetUsers_userId",
                table: "Bookmark");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Bookmark",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookmark_userId",
                table: "Bookmark",
                newName: "IX_Bookmark_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_AspNetUsers_UserId",
                table: "Bookmark",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_AspNetUsers_UserId",
                table: "Bookmark");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Bookmark",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookmark_UserId",
                table: "Bookmark",
                newName: "IX_Bookmark_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_AspNetUsers_userId",
                table: "Bookmark",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
