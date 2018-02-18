using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Common.Data.Repository;
using Common.Models.Domain;
using ViFlix.DataAccess.DbContextContainer;

namespace ViFlix.DataAccess.Repository.EFImplementation
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ViFlixContext context)
            : base(context)
        {
        }

        public ViFlixContext ViFlixContext => Context as ViFlixContext;

        public async Task<IList<Customer>> GetCustomersWithMembershipTypeAsync()
        {
            List<Entities.Customer> dBCustomers = await ViFlixContext.Customers.Include(c => c.MembershipType).ToListAsync();
            if (dBCustomers == null)
            {
                return new List<Customer>();
            }

            return dBCustomers.Select(c => new Customer
            {
                Id = c.Id,
                Name = c.Name,
                Birthday = c.Birthday,
                IsSubscribedToNewsLetter = c.IsSubscribedToNewsLetter,
                MembershipTypeId = c.MembershipTypeId,
                MembershipType = ConvertToModelMembershipType(c.MembershipType)
            }).ToList();
        }

        public async Task<Customer> GetCustomerWithMembershipTypeAsync(int id)
        {
            Entities.Customer dbCustomer = await ViFlixContext.Customers.Include(c => c.MembershipType).SingleOrDefaultAsync(c => c.Id == id);
            if (dbCustomer == null)
            {
                return new Customer();
            }

            return new Customer
            {
                Id = dbCustomer.Id,
                Name = dbCustomer.Name,
                Birthday = dbCustomer.Birthday,
                IsSubscribedToNewsLetter = dbCustomer.IsSubscribedToNewsLetter,
                MembershipTypeId = dbCustomer.MembershipTypeId,
                MembershipType = ConvertToModelMembershipType(dbCustomer.MembershipType)
            };
        }

        private static MembershipType ConvertToModelMembershipType(Entities.MembershipType membershipType)
        {
            return new MembershipType
            {
                Id = membershipType.Id,
                Name = membershipType.Name,
                DiscountRate = membershipType.DiscountRate,
                DurationInMonths = membershipType.DurationInMonths,
                SignUpFee = membershipType.SignUpFee
            };
        }
    }
}