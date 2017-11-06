using System.Data.Entity;
using ViFlix.DataAccess.Models;

namespace ViFlix.DataAccess.DbContextContainer
{
    public class ViFlixContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        public ViFlixContext()
            : base("name=ViFlixContext")
        {

        }
    }
}