using Talabat.Core.Entities;

namespace Talabat.APIs.Test1.DTOs
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public String Brand { get; set; }
        public int CategoryId { get; set; }
        public String Category { get; set; }
    }
}
