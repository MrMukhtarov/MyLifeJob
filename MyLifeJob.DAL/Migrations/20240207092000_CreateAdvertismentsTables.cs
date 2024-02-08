﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyLifeJob.DAL.Migrations
{
    public partial class CreateAdvertismentsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Advertisments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WorkGraphic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requirement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Education = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    IsPremium = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "DATEADD(hour,4,GETUTCDATE())"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advertisments_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisments_CategoryId",
                table: "Advertisments",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertisments");
        }
    }
}
