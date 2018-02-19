using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Common.Data;
using Common.Models.Domain;
using Common.Models.Dto;

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

        [Route("api/customers")]
        public async Task<IHttpActionResult> GetCustomers()
        {
            var customers = await _unitOfWork.Customers.GetAllAsync();
            IList<CustomerDto> dtoCustomers = new List<CustomerDto>(customers.Count);
            foreach (var customer in customers)
            {
                dtoCustomers.Add(Mapper.Map<Customer, CustomerDto>(customer));
            }

            return Ok(dtoCustomers);
        }

        [HttpGet]
        [Route("api/customers/{id}", Name = GetCustomerById)]
        public async Task<IHttpActionResult> GetCustomer(int id)
        {
            var customer = await _unitOfWork.Customers.GetAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        [HttpPost]
        [Route("api/customers")]
        public async Task<IHttpActionResult> CreateCustomer([FromBody] CustomerDto customer)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerToBeSaved = Mapper.Map<CustomerDto, Customer>(customer);

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

            var cstmr = await _unitOfWork.Customers.GetAsync(id);
            if (cstmr == null)
                return NotFound();

            Mapper.Map(customer, cstmr);

            await _unitOfWork.SaveAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("api/customers/{id}")]
        public async Task<IHttpActionResult> DeleteCustomer(int id)
        {
            var cstmr = await _unitOfWork.Customers.GetAsync(id);
            if (cstmr == null)
                return NotFound();

            _unitOfWork.Customers.Remove(cstmr);
            await _unitOfWork.SaveAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
        }

    }
}


