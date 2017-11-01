using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using ViFlix.DataAccess.DbContextContainer;
using ViFlix.Models;
using ViFlix.ViewModels;

namespace ViFlix.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private const string GetCustomerById = "GetCustomerById";
        private readonly ViFlixContext _context;

        public CustomersController(ViFlixContext context)
        {
            _context = context;
        }

        public CustomersController()
        {
            _context = new ViFlixContext();
        }

        [HttpGet]
        [Route("api/customers", Name = GetCustomerById)]
        public async Task<IHttpActionResult> GetCustomers()
        {
            var dbCustomers = await _context.Customers.ToListAsync();
            IList<Customer> customers = new List<Customer>(dbCustomers.Count);
            foreach (var dbCustomer in dbCustomers)
            {
                customers.Add(new Customer
                {
                    Name = dbCustomer.Name,
                    Birthday = dbCustomer.Birthday,
                    MembershipType = dbCustomer.MembershipType,
                    MembershipTypeId = dbCustomer.MembershipTypeId,
                    IsSubscribedToNewsLetter = dbCustomer.IsSubscribedToNewsLetter
                });
            }

            return Ok(customers);
        }

        [HttpGet]
        [Route("api/customers/{id}")]
        public async Task<IHttpActionResult> GetCustomer(int id)
        {
            var dbCustomer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
            if (dbCustomer == null)
                return NotFound();

            return Ok(new Customer
            {
                Name = dbCustomer.Name,
                Birthday = dbCustomer.Birthday,
                MembershipType = dbCustomer.MembershipType,
                MembershipTypeId = dbCustomer.MembershipTypeId,
            });
        }

        [HttpPost]
        [Route("api/customers")]
        public async Task<IHttpActionResult> CreateCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _context.Customers.Add(new DataAccess.Models.Customer
            {
                Name = customer.Name,
                Birthday = customer.Birthday,
                MembershipTypeId = customer.MembershipTypeId,
                MembershipType = customer.MembershipType,
                IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter
            });

            await _context.SaveChangesAsync();

            return CreatedAtRoute(GetCustomerById, new { id = customer.Id }, customer);
        }

        [HttpPut]
        [Route("api/customers/id")]
        public async Task<IHttpActionResult> UpdateCustomer(int id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var dbCustomer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
            if (dbCustomer == null)
                return NotFound();

            dbCustomer.Name = customer.Name;
            dbCustomer.Birthday = customer.Birthday;
            dbCustomer.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            dbCustomer.MembershipTypeId = customer.MembershipTypeId;
            dbCustomer.MembershipType = customer.MembershipType;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("api/customers/id")]
        public async Task<IHttpActionResult> DeleteCustomer(int id)
        {
            var dbCustomer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
            if (dbCustomer == null)
                return NotFound();

            _context.Customers.Remove(dbCustomer);
            await _context.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

    }


}


