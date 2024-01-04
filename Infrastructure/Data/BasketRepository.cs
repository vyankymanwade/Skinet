using System.Text.Json;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            this._database = redis.GetDatabase();
        }

        public async Task<bool> DeleteCustomerBasket(string basketId)
        {
            return await this._database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetCustomerBasketAsync(string basketId)
        {
            var data = await this._database.StringGetAsync(basketId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateCustomerBasketAsync(CustomerBasket basket)
        {
            bool isCreated = await this._database.StringSetAsync(basket.Id,
                JsonSerializer.Serialize(basket),TimeSpan.FromDays(30));

            if(!isCreated) return null;

            return await this.GetCustomerBasketAsync(basket.Id);
        }
    }
}