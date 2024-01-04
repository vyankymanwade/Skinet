using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepo;
        public BasketController(IBasketRepository basketRepo)
        {
            this._basketRepo = basketRepo;
        }


        [HttpGet]
        [Route("basket")]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string basketId)
        {
            var basket = await this._basketRepo.GetCustomerBasketAsync(basketId);

            return Ok(basket ?? new CustomerBasket(basketId));
        }

        [HttpPost]
        [Route("add-basket")]

        public async Task<ActionResult<CustomerBasket>> AddBasket(CustomerBasket basket)
        {
            var addedBasket = await this._basketRepo.UpdateCustomerBasketAsync(basket);

            return Ok(addedBasket);
        }

        [HttpDelete]
        [Route("delete-basket")]
        public async Task DeleteBasket(string basketId)
        {
            await this._basketRepo.DeleteCustomerBasket(basketId);
        }
    }
}