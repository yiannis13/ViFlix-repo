using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ViFlix.Models;

namespace ViFlix.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IList<Customer> _customers = new List<Customer>
        {
            new Customer(){Id = 1, Name = "Yiannis"},
            new Customer(){Id = 2, Name = "Vaia"}
        };


        [HttpGet]
        [Route("customers")]
        public ActionResult GetCustomers()
        {
            if (_customers == null)
                return HttpNotFound();

            return View(_customers);
        }

        [HttpGet]
        [Route("customers/Details/{id}")]
        public ActionResult GetCustomer(int id)
        {
            Customer customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
    }
}