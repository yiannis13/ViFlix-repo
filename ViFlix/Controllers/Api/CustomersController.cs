using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using ViFlix.DataAccess.DbContextContainer;
using ViFlix.Dtos;

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
        [Route("api/customers")]
        public async Task<IHttpActionResult> GetCustomers()
        {
            var dbCustomers = await _context.Customers.ToListAsync();
            IList<CustomerDto> customers = new List<CustomerDto>(dbCustomers.Count);
            foreach (var dbCustomer in dbCustomers)
            {
                customers.Add(Mapper.Map<DataAccess.Models.Customer, CustomerDto>(dbCustomer));
            }

            return Ok(customers);
        }

        [HttpGet]
        [Route("api/customers/{id}", Name = GetCustomerById)]
        public async Task<IHttpActionResult> GetCustomer(int id)
        {
            var dbCustomer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
            if (dbCustomer == null)
                return NotFound();

            return Ok(Mapper.Map<DataAccess.Models.Customer, CustomerDto>(dbCustomer));
        }

        [HttpPost]
        [Route("api/customers")]
        public async Task<IHttpActionResult> CreateCustomer([FromBody] CustomerDto customer)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerToBeSaved = Mapper.Map<CustomerDto, DataAccess.Models.Customer>(customer);

            _context.Customers.Add(customerToBeSaved);

            await _context.SaveChangesAsync();

            return CreatedAtRoute(GetCustomerById, new { id = customerToBeSaved.Id }, customerToBeSaved);
        }

        [HttpPut]
        [Route("api/customers/{id}")]
        public async Task<IHttpActionResult> UpdateCustomer(int id, [FromBody] CustomerDto customer)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var dbCustomer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
            if (dbCustomer == null)
                return NotFound();

            Mapper.Map(customer, dbCustomer);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("api/customers/{id}")]
        public async Task<IHttpActionResult> DeleteCustomer(int id)
        {
            var dbCustomer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
            if (dbCustomer == null)
                return NotFound();

            _context.Customers.Remove(dbCustomer);
            await _context.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

    }
}


