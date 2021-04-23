using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Migrations
{
    public partial class InitialUpgrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    TokenId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUse = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.TokenId);
                });

            migrationBuilder.CreateTable(
                name: "Containers",
                columns: table => new
                {
                    Port = table.Column<int>(type: "int", nullable: false),
                    TokenId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Command = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Containers", x => x.Port);
                    table.ForeignKey(
                        name: "FK_Containers_Tokens_TokenId",
                        column: x => x.TokenId,
                        principalTable: "Tokens",
                        principalColumn: "TokenId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Port = table.Column<int>(type: "int", nullable: false),
                    Reserved = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservedForTokenId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ApprovedByTokenId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContainerPort = table.Column<short>(type: "smallint", nullable: false),
                    Parameters = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Port);
                    table.ForeignKey(
                        name: "FK_Reservations_Tokens_ApprovedByTokenId",
                        column: x => x.ApprovedByTokenId,
                        principalTable: "Tokens",
                        principalColumn: "TokenId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Tokens_ReservedForTokenId",
                        column: x => x.ReservedForTokenId,
                        principalTable: "Tokens",
                        principalColumn: "TokenId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Containers_TokenId",
                table: "Containers",
                column: "TokenId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ApprovedByTokenId",
                table: "Reservations",
                column: "ApprovedByTokenId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservedForTokenId",
                table: "Reservations",
                column: "ReservedForTokenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Containers");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Tokens");
        }
    }
}
