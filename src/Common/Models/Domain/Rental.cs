using System;

namespace Common.Models.Domain
{
    public class Rental
    {
        public int Id { get; set; }

        public Customer Customer { get; set; }

        public Movie Movie { get; set; }

        public DateTime DateRented { get; set; }

        public DateTime? DateReturned { get; set; }

        public DateTime DateToBeReturned { get; set; }
    }
}