using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Test1.DTOs;
using Talabat.APIs.Test1.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Test1.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> productRepo;
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Product> productRepo,IMapper mapper)
        {
            this.productRepo = productRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            //var products = await productRepo.GetAllAsync(); Before Spec
            var spec = new ProductWithBrandAndCategorySpecifications();
            var products = await productRepo.GetAllWithSpecAsync(spec);
            var MappedProducts = mapper.Map<IEnumerable<Product>,IEnumerable<ProductToReturnDto>>(products);
            return Ok(MappedProducts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto),200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]


        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            //var product = await productRepo.GetAsync(id); Before Spec
            var spec = new ProductWithBrandAndCategorySpecifications(id);
            var product = await productRepo.GetByIdWithSpecAsync(spec);

            if (product is null)
                return NotFound(new ApiResponse(404));
            var MappedProduct = mapper.Map<Product, ProductToReturnDto>(product);
            return Ok(MappedProduct);
        }
    }
}
