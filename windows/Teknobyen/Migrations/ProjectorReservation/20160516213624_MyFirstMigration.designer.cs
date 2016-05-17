using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Teknobyen.Services.StorageService;

namespace Teknobyen.Migrations.ProjectorReservation
{
    [DbContext(typeof(ProjectorReservationContext))]
    [Migration("20160516213624_MyFirstMigration")]
    partial class MyFirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20896");

            modelBuilder.Entity("Teknobyen.Models.ProjectorReservationModel", b =>
                {
                    b.Property<string>("reservationId");

                    b.Property<string>("comment");

                    b.Property<DateTime>("date");

                    b.Property<DateTime>("endTime");

                    b.Property<string>("name");

                    b.Property<int>("roomNumber");

                    b.Property<DateTime>("startTime");

                    b.Property<string>("userId");

                    b.HasKey("reservationId");

                    b.ToTable("ProjectorReservations");
                });
        }
    }
}
