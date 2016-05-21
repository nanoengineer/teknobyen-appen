using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknobyen.Models;

namespace Teknobyen.Services.StorageService
{
    class LaundryBalanceContext : DbContext
    {
        public DbSet<LaundryBalanceModel> Balance { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=LaundryBalance.db");
        }
    }
}
