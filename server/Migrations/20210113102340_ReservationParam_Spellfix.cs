using Microsoft.EntityFrameworkCore.Migrations;

namespace test.Migrations
{
    public partial class ReservationParam_Spellfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Paramters",
                table: "Reservations",
                newName: "Parameters");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Parameters",
                table: "Reservations",
                newName: "Paramters");
        }
    }
}
