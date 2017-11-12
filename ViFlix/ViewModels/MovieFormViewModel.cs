using System.ComponentModel.DataAnnotations;
using ViFlix.DataAccess.Models;
using Movie = ViFlix.Models.Movie;

namespace ViFlix.ViewModels
{
    public class MovieFormViewModel
    {
        public Movie Movie { get; set; }

        [Range(1, 4, ErrorMessage = "The field Genre is required")]
        public Genre Genre { get; set; }
    }
}