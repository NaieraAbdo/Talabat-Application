﻿using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Test1.DTOs
{
    public class AddressDto
    {
        [Required]
        public int Id { get; set; }

        [Required]

        public string FName { get; set; }

        [Required]

        public string LName { get; set; }

        [Required]

        public string City { get; set; }

        [Required]

        public string Street { get; set; }

        [Required]

        public string Country { get; set; }
    }
}
