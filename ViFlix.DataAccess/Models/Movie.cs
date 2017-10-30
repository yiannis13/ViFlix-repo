using System;
using System.Collections.Generic;

namespace ViFlix.DataAccess.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public DateTime? DateAdded { get; set; }
        public int NumberInStock { get; set; }
        public Genre Genre { get; set; }
        public IList<Customer> Customers { get; set; }

        public Movie()
        {
            Customers = new List<Customer>();
        }
    }
}
