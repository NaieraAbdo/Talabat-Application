using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Test1.DTOs;
using Talabat.APIs.Test1.Errors;
using Talabat.APIs.Test1.Helper;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Test1.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> productRepo;
        private readonly IMapper mapper;
        private readonly IGenericRepository<ProductCategory> typeRepo;
        private readonly IGenericRepository<ProductBrand> brandRepo;

        public ProductsController(IGenericRepository<Product> productRepo,IMapper mapper, IGenericRepository<ProductCategory> typeRepo,IGenericRepository<ProductBrand> brandRepo)
        {
            this.productRepo = productRepo;
            this.mapper = mapper;
            this.typeRepo = typeRepo;
            this.brandRepo = brandRepo;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams Params)
        {
            //var products = await productRepo.GetAllAsync(); Before Spec
            var spec = new ProductWithBrandAndCategorySpecifications(Params);
            var products = await productRepo.GetAllWithSpecAsync(spec);
            var MappedProducts = mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products);
            var CountSpec = new ProductWithFiltrationForCountAsync(Params);
            var Count = await productRepo.GetCountWithSpecAsync(CountSpec);
            //var returnedObj = new Pagination<ProductToReturnDto>()
            //{
            //    PageIndex =Params.PageIndex,
            //    PageSize =Params.PageSize,
            //    Data =MappedProducts
            //};
            return Ok(new Pagination<ProductToReturnDto>(Params.PageIndex, Params.PageSize, MappedProducts,Count));
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

        //Get All Types
        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetType()
        {
            var Types = await typeRepo.GetAllAsync();
            return Ok(Types);
        }

        //Get All Brands
        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrand()
        {
            var Brands = await brandRepo.GetAllAsync();
            return Ok(Brands);
        }
    }
}
