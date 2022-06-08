using Basket.API.Entities;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisChache;

        public BasketRepository(IDistributedCache distributedCache)
        {
            _redisChache = distributedCache;
        }
        public Task DeleteBasket(string userName)
        {
           return _redisChache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _redisChache.GetStringAsync(userName);

            if (string.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redisChache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }
    }
}
