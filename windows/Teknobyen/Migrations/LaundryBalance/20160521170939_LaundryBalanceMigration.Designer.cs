using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Teknobyen.Services.StorageService;

namespace Teknobyen.Migrations.LaundryBalance
{
    [DbContext(typeof(LaundryBalanceContext))]
    [Migration("20160521170939_LaundryBalanceMigration")]
    partial class LaundryBalanceMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20896");

            modelBuilder.Entity("Teknobyen.Models.LaundryBalanceModel", b =>
                {
                    b.Property<DateTime>("Retrieved");

                    b.Property<double>("Balance");

                    b.HasKey("Retrieved");

                    b.ToTable("Balance");
                });
        }
    }
}
