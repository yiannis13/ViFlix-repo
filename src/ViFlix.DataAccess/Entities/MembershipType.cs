﻿using System.ComponentModel.DataAnnotations;

namespace ViFlix.DataAccess.Entities
{
    public class MembershipType
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public int SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }

        [Range(0, 100)]
        public byte DiscountRate { get; set; }

        public enum Type : byte
        {
            Unknown = 0,
            PayAsYouGo = 1,
            Monthly = 2,
            Quarterly = 3,
            Annual = 4
        }

    }

}