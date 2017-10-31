﻿using System;
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

        [Display(Name = "Date of Birth")]
        public DateTime? Birthday { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        public MembershipType MembershipType { get; set; }

        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }

        public IList<Movie> Movies { get; set; }

        public Customer()
        {
            Movies = new List<Movie>();
        }
    }
}