using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ShippingDate",
                table: "Shippings",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Shippings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Packages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_CityId",
                table: "Shippings",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_CityId",
                table: "Packages",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Cities_CityId",
                table: "Packages",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shippings_Cities_CityId",
                table: "Shippings",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Cities_CityId",
                table: "Packages");

            migrationBuilder.DropForeignKey(
                name: "FK_Shippings_Cities_CityId",
                table: "Shippings");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Shippings_CityId",
                table: "Shippings");

            migrationBuilder.DropIndex(
                name: "IX_Packages_CityId",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Shippings");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Packages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ShippingDate",
                table: "Shippings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
