using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Common.Data;
using Common.Models;
using Common.Models.Domain;
using ViFlix.ViewModels;

namespace ViFlix.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize]
        [Route("customers")]
        public async Task<ActionResult> GetCustomers()
        {
            IList<Customer> customers = await _unitOfWork.Customers.GetCustomersWithMembershipTypeAsync();
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
            IEnumerable<MembershipType> membershipTypes = await _unitOfWork.MembershipTypes.GetAllAsync();
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
                    MembershipTypes = await _unitOfWork.MembershipTypes.GetAllAsync()
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

            _unitOfWork.Customers.Add(customer);

            await _unitOfWork.SaveAsync();

            return RedirectToAction("GetCustomers");
        }

        [HttpGet]
        [Authorize(Roles = RoleName.Admin)]
        [Route("customers/edit/{id}")]
        public async Task<ActionResult> EditCustomerForm(int id)
        {
            var cstmr = await _unitOfWork.Customers.GetCustomerWithMembershipTypeAsync(id);
            if (cstmr == null)
                return HttpNotFound();

            var customer = new Customer
            {
                Id = cstmr.Id,
                Name = cstmr.Name,
                Birthday = cstmr.Birthday,
                IsSubscribedToNewsLetter = cstmr.IsSubscribedToNewsLetter,
                MembershipType = cstmr.MembershipType,
                MembershipTypeId = cstmr.MembershipTypeId
            };

            var oldCustomerViewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = await _unitOfWork.MembershipTypes.GetAllAsync()
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
                    MembershipTypes = await _unitOfWork.MembershipTypes.GetAllAsync()
                };

                return View("EditCustomerForm", model);
            }

            var oldCustomer = await _unitOfWork.Customers.GetAsync(viewModel.Customer.Id);
            if (oldCustomer == null)
                return HttpNotFound();

            oldCustomer.Name = viewModel.Customer.Name;
            oldCustomer.Birthday = viewModel.Customer.Birthday;
            oldCustomer.IsSubscribedToNewsLetter = viewModel.Customer.IsSubscribedToNewsLetter;
            oldCustomer.MembershipTypeId = viewModel.Customer.MembershipTypeId;

            await _unitOfWork.SaveAsync();

            return RedirectToAction("GetCustomers");
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
        }

    }
}