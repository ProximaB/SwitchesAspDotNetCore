using MoviesAPI.DB.DbModels;
using System.Data.Entity;

namespace MoviesAPI.DB
{
    public class MoviesContext : DbContext
    {
        public MoviesContext() : base("Data Source=.;Initial Catalog=MoviesAPIDb;Integrated Security=True")
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
