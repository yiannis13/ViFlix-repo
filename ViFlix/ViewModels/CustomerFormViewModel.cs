using System.Collections.Generic;
using ViFlix.DataAccess.Models;
using Customer = ViFlix.Models.Customer;

namespace ViFlix.ViewModels
{
    public class CustomerFormViewModel
    {
        public Customer Customer { get; set; }
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
    }
}