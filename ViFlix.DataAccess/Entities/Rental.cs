using System;
using System.ComponentModel.DataAnnotations;

namespace ViFlix.DataAccess.Entities
{
    public class Rental
    {
        public int Id { get; set; }

        [Required]
        public Customer Customer { get; set; }

        [Required]
        public Movie Movie { get; set; }

        public DateTime DateRented { get; set; }

        public DateTime? DateReturned { get; set; }

        public DateTime DateToBeReturned { get; set; }
    }
}