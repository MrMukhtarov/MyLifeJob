using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyLifeJob.DAL.Migrations
{
    public partial class ConnectComoanyToAdvertisments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Advertisments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Advertisments_CompanyId",
                table: "Advertisments",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisments_Companies_CompanyId",
                table: "Advertisments",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisments_Companies_CompanyId",
                table: "Advertisments");

            migrationBuilder.DropIndex(
                name: "IX_Advertisments_CompanyId",
                table: "Advertisments");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Advertisments");
        }
    }
}
