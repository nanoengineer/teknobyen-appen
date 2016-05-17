using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Teknobyen.Services.StorageService;

namespace Teknobyen.Migrations
{
    [DbContext(typeof(WashlistContext))]
    [Migration("20160516213555_MyFirstMigration")]
    partial class MyFirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20896");

            modelBuilder.Entity("Teknobyen.Models.WashDayModel", b =>
                {
                    b.Property<string>("FBID");

                    b.Property<int>("Assignment");

                    b.Property<DateTime>("Date");

                    b.Property<int>("RoomNumber");

                    b.HasKey("FBID");

                    b.ToTable("Washdays");
                });
        }
    }
}
