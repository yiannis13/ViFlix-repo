using System.ComponentModel.DataAnnotations;
using Common.Models.Domain;

namespace ViFlix.ViewModels
{
    public class MovieFormViewModel
    {
        public Movie Movie { get; set; }

        [Range(1, 4, ErrorMessage = "The field Genre is required")]
        public Genre Genre { get; set; }
    }
}