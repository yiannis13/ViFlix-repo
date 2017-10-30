using System;
using System.Collections.Generic;

namespace ViFlix.DataAccess.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }
        public MembershipType MembershipType { get; set; }
        public IList<Movie> Movies { get; set; }
        public byte MembershipTypeId { get; set; }

        public Customer()
        {
            Movies = new List<Movie>();
        }
    }
}
