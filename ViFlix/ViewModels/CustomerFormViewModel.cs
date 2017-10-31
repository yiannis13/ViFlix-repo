using System.Collections.Generic;
using ViFlix.DataAccess.Models;

namespace ViFlix.ViewModels
{
    public class CustomerFormViewModel
    {
        public Customer Customer { get; set; }
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
    }
}