using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Test1.DTOs
{
    public class CustomerBasketDto
    {
        [Required]
        public int Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}
