using SwitchesAPI.DB.DbModels;
using System.Data.Entity;

namespace SwitchesAPI.DB
{
    public class SwitchesContext : DbContext
    {
        public SwitchesContext() : base("Data Source=(localdb)\\ProjectsV13;Initial Catalog=SwitchesAPIDb2;Integrated Security=True")
        {
        }

        public DbSet<Room> Rooms{ get; set; }
        public DbSet<Switch> Switches{ get; set; }
    }
}
