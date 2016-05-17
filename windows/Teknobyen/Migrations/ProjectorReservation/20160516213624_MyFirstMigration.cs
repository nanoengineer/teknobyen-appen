using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Teknobyen.Migrations.ProjectorReservation
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectorReservations",
                columns: table => new
                {
                    reservationId = table.Column<string>(nullable: false),
                    comment = table.Column<string>(nullable: true),
                    date = table.Column<DateTime>(nullable: false),
                    endTime = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    roomNumber = table.Column<int>(nullable: false),
                    startTime = table.Column<DateTime>(nullable: false),
                    userId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectorReservations", x => x.reservationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectorReservations");
        }
    }
}
