using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.APIs.Test1.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> productRepo;

        public ProductsController(IGenericRepository<Product> productRepo)
        {
            this.productRepo = productRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await productRepo.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await productRepo.GetAsync(id);
            if (product is null)
                return NotFound();
            return Ok(product);
        }
    }
}
