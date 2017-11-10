using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ViFlix.Dtos
{
    public class RentalDto
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public IList<string> MovieNames { get; set; }

    }
}