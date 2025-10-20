using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations.Api
{
    /// <inheritdoc />
    public partial class ApiInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    MailBox = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.MailBox);
                });

            migrationBuilder.CreateTable(
                name: "ApiAuthTokens",
                columns: table => new
                {
                    ApiAuthTokenGuid = table.Column<string>(type: "TEXT", nullable: false),
                    AuthToken = table.Column<string>(type: "TEXT", nullable: false),
                    UserGuid = table.Column<string>(type: "TEXT", nullable: false),
                    CreateTimestamp = table.Column<long>(type: "INTEGER", nullable: false),
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiAuthTokens", x => x.ApiAuthTokenGuid);
                });

            migrationBuilder.CreateTable(
                name: "FriendsCardCheckList",
                columns: table => new
                {
                    Guid = table.Column<string>(type: "TEXT", nullable: false),
                    CardPictureUrl = table.Column<string>(type: "TEXT", nullable: false),
                    CardLink = table.Column<string>(type: "TEXT", nullable: false),
                    TitleDict = table.Column<string>(type: "TEXT", nullable: false),
                    CommentDict = table.Column<string>(type: "TEXT", nullable: false),
                    AddOn = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendsCardCheckList", x => x.Guid);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                column: "MailBox",
                value: "1969154690@qq.com");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "ApiAuthTokens");

            migrationBuilder.DropTable(
                name: "FriendsCardCheckList");
        }
    }
}
