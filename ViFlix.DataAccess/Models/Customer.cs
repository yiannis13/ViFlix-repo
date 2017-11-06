using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViFlix.DataAccess.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public DateTime? Birthday { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        public MembershipType MembershipType { get; set; }

        public byte MembershipTypeId { get; set; }

    }
}
