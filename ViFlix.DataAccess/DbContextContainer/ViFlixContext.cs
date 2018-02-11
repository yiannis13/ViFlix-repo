using System.Data.Entity;
using Common.Models.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ViFlix.DataAccess.Entities;

namespace ViFlix.DataAccess.DbContextContainer
{
    public class ViFlixContext : IdentityDbContext<AppUser>
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