using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViFlix.DataAccess.DbContextContainer;
using ViFlix.DataAccess.Models;
using ViFlix.ViewModels;

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
        public async Task<ActionResult> GetCustomers()
        {
            if (_context == null)
                return HttpNotFound();

            IList<Customer> customers = await _context.Customers.Include(c => c.MembershipType).ToListAsync();
            if (!customers.Any())
                return HttpNotFound();

            return View(customers);
        }

        [HttpGet]
        [Route("customers/Details/{id}")]
        public async Task<ActionResult> GetCustomer(int id)
        {
            var customer = await _context.Customers.Include(c => c.MembershipType).SingleOrDefaultAsync(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        [HttpGet]
        [Route("customers/new")]
        public ActionResult CreateCustomerForm()
        {
            IEnumerable<MembershipType> membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomer(CustomerFormViewModel viewModel)
        {
            _context.Customers.Add(viewModel.Customer);

            await _context.SaveChangesAsync();

            return RedirectToAction("GetCustomers");
        }

        [HttpGet]
        [Route("customers/edit/{id}")]
        public async Task<ActionResult> EditCustomerForm(int id)
        {
            var dBCustomer = await _context.Customers.Include(c => c.MembershipType).SingleOrDefaultAsync(c => c.Id == id);
            if (dBCustomer == null)
                return HttpNotFound();

            var oldCustomerViewModel = new CustomerFormViewModel
            {
                Customer = dBCustomer,
                MembershipTypes = await _context.MembershipTypes.ToListAsync()
            };
            return View(oldCustomerViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditCustomer(CustomerFormViewModel viewModel)
        {
            var oldCustomer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == viewModel.Customer.Id);
            if (oldCustomer == null)
                return HttpNotFound();

            oldCustomer.Name = viewModel.Customer.Name;
            oldCustomer.Birthday = viewModel.Customer.Birthday;
            oldCustomer.IsSubscribedToNewsLetter = viewModel.Customer.IsSubscribedToNewsLetter;
            oldCustomer.MembershipTypeId = viewModel.Customer.MembershipTypeId;

            await _context.SaveChangesAsync();

            return RedirectToAction("GetCustomers");
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

    }
}