using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyLifeJob.DAL.Migrations
{
    public partial class UpdateAdvertismentTablesDeleteUnknownSalaryProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnknownSalary",
                table: "Advertisments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnknownSalary",
                table: "Advertisments",
                type: "int",
                nullable: true);
        }
    }
}
