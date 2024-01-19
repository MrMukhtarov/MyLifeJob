using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyLifeJob.DAL.Migrations
{
    public partial class UpdateCompanyIndustryDatvles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyIndustries_Companies_CompanyId",
                table: "CompanyIndustries");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyIndustries_Industries_IndustryId",
                table: "CompanyIndustries");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyIndustries_Companies_CompanyId",
                table: "CompanyIndustries",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyIndustries_Industries_IndustryId",
                table: "CompanyIndustries",
                column: "IndustryId",
                principalTable: "Industries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyIndustries_Companies_CompanyId",
                table: "CompanyIndustries");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyIndustries_Industries_IndustryId",
                table: "CompanyIndustries");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyIndustries_Companies_CompanyId",
                table: "CompanyIndustries",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyIndustries_Industries_IndustryId",
                table: "CompanyIndustries",
                column: "IndustryId",
                principalTable: "Industries",
                principalColumn: "Id");
        }
    }
}
