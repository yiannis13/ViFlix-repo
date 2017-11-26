using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using ViFlix.DataAccess.DbContextContainer;
using ViFlix.Dtos;
using ViFlix.Repository;
using ViFlix.Repository.EFImplementation;

namespace ViFlix.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private const string GetCustomerById = "GetCustomerById";

        public CustomersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CustomersController()
        {
            _unitOfWork = new UnitOfWork(new ViFlixContext());
        }

        [HttpGet]
        [Route("api/customers")]
        public async Task<IHttpActionResult> GetCustomers()
        {
            var dbCustomers = await _unitOfWork.Customers.GetAllAsync();
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
            var dbCustomer = await _unitOfWork.Customers.GetAsync(id);
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

            _unitOfWork.Customers.Add(customerToBeSaved);

            await _unitOfWork.SaveAsync();

            return CreatedAtRoute(GetCustomerById, new { id = customerToBeSaved.Id }, customerToBeSaved);
        }

        [HttpPut]
        [Route("api/customers/{id}")]
        public async Task<IHttpActionResult> UpdateCustomer(int id, [FromBody] CustomerDto customer)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var dbCustomer = await _unitOfWork.Customers.GetAsync(id);
            if (dbCustomer == null)
                return NotFound();

            Mapper.Map(customer, dbCustomer);

            await _unitOfWork.SaveAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("api/customers/{id}")]
        public async Task<IHttpActionResult> DeleteCustomer(int id)
        {
            var dbCustomer = await _unitOfWork.Customers.GetAsync(id);
            if (dbCustomer == null)
                return NotFound();

            _unitOfWork.Customers.Remove(dbCustomer);
            await _unitOfWork.SaveAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
        }

    }
}


