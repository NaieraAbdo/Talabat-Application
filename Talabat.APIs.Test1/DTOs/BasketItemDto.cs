﻿using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Test1.DTOs
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]

        public string ProductName { get; set; }
        [Required]

        public string PictureUrl { get; set; }
        [Required]

        public string Brand { get; set; }
        [Required]

        public string Type { get; set; }
        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage ="Price can't be Zero.")]
        public decimal Price { get; set; }
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Quantity must be one item at least.")]
        public int Quantity { get; set; }
    }
}