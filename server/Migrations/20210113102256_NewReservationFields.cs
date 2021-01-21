using Microsoft.EntityFrameworkCore.Migrations;

namespace test.Migrations
{
    public partial class NewReservationFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "ContainerPort",
                table: "Reservations",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Paramters",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContainerPort",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Paramters",
                table: "Reservations");
        }
    }
}
