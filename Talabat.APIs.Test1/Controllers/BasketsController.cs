using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Test1.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.APIs.Test1.Controllers
{
    public class BasketsController :BaseApiController
    {
        private readonly IBasketRepository basketRepository;

        public BasketsController(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }
        [HttpGet("{BasketId}")]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string BasketId)
        {
            var basket =await basketRepository.GetBasketAsync(BasketId);
            if (basket is null) return new CustomerBasket(BasketId);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var CreatedOrUpdatedBasket = await basketRepository.UpdateBasketAsync(basket);
            if (CreatedOrUpdatedBasket is null) return BadRequest(new ApiResponse(400));
            return Ok(CreatedOrUpdatedBasket); //or return CreatedOrUpdatedBasket
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket (string BasketId)
        {
            return await basketRepository.DeleteBasketAsync(BasketId);
        }
    }
}
