using System.Collections.Generic;
using ViFlix.Models;

namespace ViFlix.ViewModels
{
    public class RandomMovieViewModel
    {
        public Movie Movie { get; set; }
        public IList<Customer> Customers { get; set; }
    }
}