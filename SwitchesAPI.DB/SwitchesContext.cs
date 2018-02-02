using SwitchesAPI.DB.DbModels;
using System.Data.Entity;

using System.Runtime.Remoting;

namespace SwitchesAPI.DB
{
    public class SwitchesContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Switch> Switches { get; set; }

        public SwitchesContext () : base(nameOrConnectionString: "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SwitchesAPIDb;Integrated Security=True")
        {
        }

        protected override void OnModelCreating (DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasMany<Switch>(u => u.Switches)
                .WithMany(s => s.Users)
                .Map(cs =>
                {
                    cs.MapLeftKey("Id");
                    cs.MapRightKey("SwitchId");
                    cs.ToTable("UserSwitches");
                });

        }


    }
}
