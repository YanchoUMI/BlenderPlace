using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlenderPlace.Migrations
{
    /// <inheritdoc />
    public partial class UserFavoriteTutorials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFavoriteTutorials",
                columns: table => new
                {
                    FavoriteTutorialsId = table.Column<int>(type: "int", nullable: false),
                    FavoritedByUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteTutorials", x => new { x.FavoriteTutorialsId, x.FavoritedByUsersId });
                    table.ForeignKey(
                        name: "FK_UserFavoriteTutorials_AspNetUsers_FavoritedByUsersId",
                        column: x => x.FavoritedByUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavoriteTutorials_Tutorials_FavoriteTutorialsId",
                        column: x => x.FavoriteTutorialsId,
                        principalTable: "Tutorials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteTutorials_FavoritedByUsersId",
                table: "UserFavoriteTutorials",
                column: "FavoritedByUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFavoriteTutorials");
        }
    }
}
