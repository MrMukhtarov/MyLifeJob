using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyLifeJob.DAL.Migrations
{
    public partial class CreateTextsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "Advertisments");

            migrationBuilder.CreateTable(
                name: "Texts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdvertismentId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Texts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Texts_Advertisments_AdvertismentId",
                        column: x => x.AdvertismentId,
                        principalTable: "Advertisments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Texts_AdvertismentId",
                table: "Texts",
                column: "AdvertismentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Texts");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Advertisments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
