using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyLifeJob.DAL.Migrations
{
    public partial class CreateAdvertismentAbilityTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ability",
                table: "Advertisments");

            migrationBuilder.CreateTable(
                name: "Ability",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ability", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdvertismentAbilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AbilityId = table.Column<int>(type: "int", nullable: false),
                    AdvertismentId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertismentAbilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertismentAbilities_Ability_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "Ability",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdvertismentAbilities_Advertisments_AdvertismentId",
                        column: x => x.AdvertismentId,
                        principalTable: "Advertisments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvertismentAbilities_AbilityId",
                table: "AdvertismentAbilities",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertismentAbilities_AdvertismentId",
                table: "AdvertismentAbilities",
                column: "AdvertismentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertismentAbilities");

            migrationBuilder.DropTable(
                name: "Ability");

            migrationBuilder.AddColumn<string>(
                name: "Ability",
                table: "Advertisments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
