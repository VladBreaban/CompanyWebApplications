using DatabaseInteractions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseInteractions
{
    public class CompanyDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }

        public DbSet<User> Users { get; set; }

        public CompanyDbContext() : base() { }
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Company>(entity => {
                entity.HasIndex(e => e.Isin).IsUnique();
            });

            builder.Entity<User>(entity => {
                entity.HasIndex(e => e.email).IsUnique();
            });

        }
    }
}
