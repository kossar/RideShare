using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.App.EF.Migrations
{
    public partial class ScheduleFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_TransportNeed_TransportNeedId",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_TransportOffer_TransportOfferId",
                table: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_Schedule_TransportNeedId",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "TransportNeedId",
                table: "Schedule");

            migrationBuilder.AddColumn<Guid>(
                name: "ScheduleId",
                table: "TransportOffer",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartAt",
                table: "TransportOffer",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ScheduleId",
                table: "TransportNeed",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartAt",
                table: "TransportNeed",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Schedule",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_TransportOffer_ScheduleId",
                table: "TransportOffer",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportNeed_ScheduleId",
                table: "TransportNeed",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_TransportOffer_TransportOfferId",
                table: "Schedule",
                column: "TransportOfferId",
                principalTable: "TransportOffer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransportNeed_Schedule_ScheduleId",
                table: "TransportNeed",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportOffer_Schedule_ScheduleId",
                table: "TransportOffer",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_TransportOffer_TransportOfferId",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportNeed_Schedule_ScheduleId",
                table: "TransportNeed");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportOffer_Schedule_ScheduleId",
                table: "TransportOffer");

            migrationBuilder.DropIndex(
                name: "IX_TransportOffer_ScheduleId",
                table: "TransportOffer");

            migrationBuilder.DropIndex(
                name: "IX_TransportNeed_ScheduleId",
                table: "TransportNeed");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "TransportOffer");

            migrationBuilder.DropColumn(
                name: "StartAt",
                table: "TransportOffer");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "TransportNeed");

            migrationBuilder.DropColumn(
                name: "StartAt",
                table: "TransportNeed");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Schedule",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<Guid>(
                name: "TransportNeedId",
                table: "Schedule",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_TransportNeedId",
                table: "Schedule",
                column: "TransportNeedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_TransportNeed_TransportNeedId",
                table: "Schedule",
                column: "TransportNeedId",
                principalTable: "TransportNeed",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_TransportOffer_TransportOfferId",
                table: "Schedule",
                column: "TransportOfferId",
                principalTable: "TransportOffer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
