using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViFlix.DataAccess.DbContextContainer;
using ViFlix.DataAccess.Models;
using ViFlix.Models;
using ViFlix.ViewModels;
using Customer = ViFlix.DataAccess.Models.Customer;

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
        [Authorize]
        [Route("customers")]
        public async Task<ActionResult> GetCustomers()
        {
            IList<Customer> customers = await _context.Customers.Include(c => c.MembershipType).ToListAsync();
            if (!customers.Any())
                return HttpNotFound();

            if (User.IsInRole(RoleName.Admin))
                return View("GetCustomers", customers);

            return View("GetCustomersReadOnly", customers);
        }

        [HttpGet]
        [Authorize(Roles = RoleName.Admin)]
        [Route("customers/new")]
        public async Task<ViewResult> CreateCustomerForm()
        {
            IEnumerable<MembershipType> membershipTypes = await _context.MembershipTypes.ToListAsync();
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };

            return View(viewModel);
        }


        [HttpPost]
        [Authorize(Roles = RoleName.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCustomer(CustomerFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var model = new CustomerFormViewModel
                {
                    Customer = viewModel.Customer,
                    MembershipTypes = await _context.MembershipTypes.ToListAsync()
                };

                return View("CreateCustomerForm", model);
            }

            var customer = new Customer
            {
                Name = viewModel.Customer.Name,
                Birthday = viewModel.Customer.Birthday,
                MembershipType = viewModel.Customer.MembershipType,
                IsSubscribedToNewsLetter = viewModel.Customer.IsSubscribedToNewsLetter,
                MembershipTypeId = viewModel.Customer.MembershipTypeId
            };

            _context.Customers.Add(customer);

            await _context.SaveChangesAsync();

            return RedirectToAction("GetCustomers");
        }

        [HttpGet]
        [Authorize(Roles = RoleName.Admin)]
        [Route("customers/edit/{id}")]
        public async Task<ActionResult> EditCustomerForm(int id)
        {
            var dBCustomer = await _context.Customers.Include(c => c.MembershipType).SingleOrDefaultAsync(c => c.Id == id);
            if (dBCustomer == null)
                return HttpNotFound();

            var customer = new Models.Customer
            {
                Id = dBCustomer.Id,
                Name = dBCustomer.Name,
                Birthday = dBCustomer.Birthday,
                IsSubscribedToNewsLetter = dBCustomer.IsSubscribedToNewsLetter,
                MembershipType = dBCustomer.MembershipType,
                MembershipTypeId = dBCustomer.MembershipTypeId
            };

            var oldCustomerViewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = await _context.MembershipTypes.ToListAsync()
            };
            return View(oldCustomerViewModel);
        }

        [HttpPost]
        [Authorize(Roles = RoleName.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCustomer(CustomerFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var model = new CustomerFormViewModel
                {
                    Customer = viewModel.Customer,
                    MembershipTypes = await _context.MembershipTypes.ToListAsync()
                };

                return View("EditCustomerForm", model);
            }

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