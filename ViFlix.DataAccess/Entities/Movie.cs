using System;
using System.ComponentModel.DataAnnotations;

namespace ViFlix.DataAccess.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime? ReleasedDate { get; set; }

        public DateTime? DateAdded { get; set; }

        [Required]
        public int NumberInStock { get; set; }

        [Required]
        public Genre Genre { get; set; }

        public int NumberAvailable { get; set; }

    }
}
