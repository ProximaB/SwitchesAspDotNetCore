using SwitchesAPI.DB.DbModels;
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

            modelBuilder.Entity<UserSwitch>()
                .HasKey(e => new {e.UserId, e.SwitchId});

        }


    }
}
