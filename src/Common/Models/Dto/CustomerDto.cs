using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Common.Models.Dto
{
    public class CustomerDto
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public DateTime? Birthday { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        public byte MembershipTypeId { get; set; }
    }
}