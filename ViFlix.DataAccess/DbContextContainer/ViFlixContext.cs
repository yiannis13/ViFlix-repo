using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using ViFlix.DataAccess.Identity;
using ViFlix.DataAccess.Models;

namespace ViFlix.DataAccess.DbContextContainer
{
    public class ViFlixContext : IdentityDbContext<AppUser>
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }

        public ViFlixContext()
            : base("name=ViFlixContext")
        {

        }
    }
}