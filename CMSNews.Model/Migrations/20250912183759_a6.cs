using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSNews.Model.Migrations
{
    /// <inheritdoc />
    public partial class a6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblNews_tblNewsGroup_tblNewsGroupId",
                table: "tblNews");

            migrationBuilder.RenameColumn(
                name: "tblNewsGroupId",
                table: "tblNews",
                newName: "NewsGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_tblNews_tblNewsGroupId",
                table: "tblNews",
                newName: "IX_tblNews_NewsGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblNews_tblNewsGroup_NewsGroupId",
                table: "tblNews",
                column: "NewsGroupId",
                principalTable: "tblNewsGroup",
                principalColumn: "NewsGroupId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblNews_tblNewsGroup_NewsGroupId",
                table: "tblNews");

            migrationBuilder.RenameColumn(
                name: "NewsGroupId",
                table: "tblNews",
                newName: "tblNewsGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_tblNews_NewsGroupId",
                table: "tblNews",
                newName: "IX_tblNews_tblNewsGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblNews_tblNewsGroup_tblNewsGroupId",
                table: "tblNews",
                column: "tblNewsGroupId",
                principalTable: "tblNewsGroup",
                principalColumn: "NewsGroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
