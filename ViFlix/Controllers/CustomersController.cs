using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ViFlix.DataAccess.DbContextContainer;
using ViFlix.DataAccess.Models;

namespace ViFlix.Controllers
{
    public class CustomersController : Controller
    {
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
        [Route("customers")]
        public ActionResult GetCustomers()
        {
            if (_context == null)
                return HttpNotFound();

            IList<Customer> customers = _context.Customers.Include(c => c.MembershipType).ToList();
            if (!customers.Any())
                return HttpNotFound();

            return View(customers);
        }

        [HttpGet]
        [Route("customers/Details/{id}")]
        public ActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}