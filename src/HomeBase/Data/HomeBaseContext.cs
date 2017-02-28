using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeBase.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeBase.Data
{
    public class HomeBaseContext : DbContext
    {
        public HomeBaseContext(DbContextOptions<HomeBaseContext> options) : base(options)
        {

        }

        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Captain> Captains { get; set; }
        public DbSet<TeamAssignment> TeamAssignment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment", "Admin");
            modelBuilder.Entity<Player>().ToTable("Player", "User");
            modelBuilder.Entity<Team>().ToTable("Team", "User");
            modelBuilder.Entity<Captain>().ToTable("Captain", "Admin");
            modelBuilder.Entity<TeamAssignment>().ToTable("TeamAssignment","Admin");
            //Composite primary key from two FK
            modelBuilder.Entity<TeamAssignment>().HasKey(t => new { t.TeamID, t.CaptainID });
        }

    }
}
