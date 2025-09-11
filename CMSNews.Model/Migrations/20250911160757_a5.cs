using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSNews.Model.Migrations
{
    /// <inheritdoc />
    public partial class a5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "tblUser",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "tblNewsGroup",
                newName: "NewsGroupId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "tblNews",
                newName: "NewsId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "tblComment",
                newName: "CommentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "tblUser",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "NewsGroupId",
                table: "tblNewsGroup",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "NewsId",
                table: "tblNews",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "tblComment",
                newName: "id");
        }
    }
}
