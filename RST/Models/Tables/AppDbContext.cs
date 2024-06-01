using Microsoft.EntityFrameworkCore;
using System;

namespace RST.Models.Tables
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options) { }

        public DbSet<Customer> customers { get; set; }
        public DbSet<Agent> agents { get; set; }
        public DbSet<Apartment> apartments { get; set; }
        public DbSet<SoldApartments> soldApartments { get; set; }

    }
}
