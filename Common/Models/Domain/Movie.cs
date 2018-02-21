using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Models.Domain
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Release Date")]
        public DateTime? ReleasedDate { get; set; }

        [Display(Name = "Number In Stock")]
        [Range(1, 20)]
        public int NumberInStock { get; set; }

        public int NumberAvailable { get; set; }

        public Genre Genre { get; set; }

        public DateTime DateAdded { get; set; }
    }
}