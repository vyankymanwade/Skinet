using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetCustomerBasketAsync(string basketId);
        Task<CustomerBasket> UpdateCustomerBasketAsync(CustomerBasket basket);

        Task<bool> DeleteCustomerBasket(string basketId);
    }
}