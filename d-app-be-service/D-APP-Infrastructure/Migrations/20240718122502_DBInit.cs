using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D_APP_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DBInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserFname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserLname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserDateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserModifiDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserRole = table.Column<byte>(type: "tinyint", nullable: true),
                    UserStatus = table.Column<byte>(type: "tinyint", nullable: true),
                    UserPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DocTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocType = table.Column<byte>(type: "tinyint", nullable: true),
                    DocCreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DocAuthor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocCont = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocModifiDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DocStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocFolderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocId);
                    table.ForeignKey(
                        name: "FK_Documents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_UserId",
                table: "Documents",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
