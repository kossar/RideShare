using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.App.EF.Migrations
{
    public partial class UpdateSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transports_Location_DestinationLocationId",
                table: "Transports");

            migrationBuilder.DropForeignKey(
                name: "FK_Transports_Location_StartLocationId",
                table: "Transports");

            migrationBuilder.DropIndex(
                name: "IX_Transports_DestinationLocationId",
                table: "Transports");

            migrationBuilder.DropIndex(
                name: "IX_Transports_StartLocationId",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "DestinationLocationId",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "StartLocationId",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "ScheduledAt",
                table: "Schedule");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Schedule",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Schedule");

            migrationBuilder.AddColumn<Guid>(
                name: "DestinationLocationId",
                table: "Transports",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StartLocationId",
                table: "Transports",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledAt",
                table: "Schedule",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Transports_DestinationLocationId",
                table: "Transports",
                column: "DestinationLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_StartLocationId",
                table: "Transports",
                column: "StartLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transports_Location_DestinationLocationId",
                table: "Transports",
                column: "DestinationLocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transports_Location_StartLocationId",
                table: "Transports",
                column: "StartLocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
