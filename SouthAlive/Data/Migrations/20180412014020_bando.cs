using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SouthAlive.Data.Migrations
{
    public partial class bando : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EventDetail_VenueBookingID",
                table: "EventDetail");

            migrationBuilder.CreateIndex(
                name: "IX_EventDetail_VenueBookingID",
                table: "EventDetail",
                column: "VenueBookingID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EventDetail_VenueBookingID",
                table: "EventDetail");

            migrationBuilder.CreateIndex(
                name: "IX_EventDetail_VenueBookingID",
                table: "EventDetail",
                column: "VenueBookingID");
        }
    }
}
