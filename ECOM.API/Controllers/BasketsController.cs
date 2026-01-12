using AutoMapper;
using ECOM.API.Helper;
using ECOM.CORE.Entites;
using ECOM.CORE.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECOM.API.Controllers
{

    public class BasketsController : BaseController
    {
        public BasketsController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }
        [HttpGet("get-basket-item/{id}")]
        public async Task<IActionResult> get(string id)
        {
            var result = await work.CustomerBasketRepository.GetBasketAsync(id);
            if(result is null)
            {
                return Ok(new CustomerBasket());
            }
            return Ok(result);
        }
        [HttpPost("update-basket")]
        public async Task<IActionResult> add(CustomerBasket basket)
        {
            var _basket=await work.CustomerBasketRepository.UpdateBasketAsync(basket);
            return Ok(basket);
        }
        [HttpDelete("delete-basket-item/{id}")]
        public async Task<IActionResult> delete(string id)
        {
            var result=await work.CustomerBasketRepository.DeleteBasketAsync(id);
            return result ? Ok(new ResponseAPI(200,"Item Deleted")):BadRequest(new ResponseAPI(400));
        }
    }
}
