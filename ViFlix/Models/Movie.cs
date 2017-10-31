using System;
using System.ComponentModel.DataAnnotations;

namespace ViFlix.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Release Date")]
        public DateTime? ReleasedDate { get; set; }

        [Display(Name = "Number In Stock")]
        public int NumberInStock { get; set; }

    }
}
