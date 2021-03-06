﻿using SwitchesAPI.DB.DbModels;
using Microsoft.EntityFrameworkCore;

using SwitchesAPI.Models;

namespace SwitchesAPI.DB
{
    public class SwitchesContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Switch> Switches { get; set; }
        public DbSet<UserSwitch> UserSwitches { get; set; }
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SwitchesAPIDb;Integrated Security=True");
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(b => b.UserName)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            //modelBuilder.Entity<User>()
            //    .HasKey(a => new { a.Id, a.Name });

            modelBuilder.Entity<UserSwitch>()
                    .HasKey(e => new {e.UserName, e.SwitchId});
        }
    }
}
