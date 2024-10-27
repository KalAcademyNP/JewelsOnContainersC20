using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using WebMVC.Infrastructure;
using WebMVC.Models;
using WebMVC.Models.CartModels;

namespace WebMVC.Services
{
    public class CartService : ICartService
    {
        private readonly string _baseUrl;
        private readonly IConfiguration _config;
        private readonly IHttpClient _apiClient;
        private readonly IHttpContextAccessor _httpContextAccesor;
        public CartService(IConfiguration config, IHttpClient client, 
            IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _apiClient = client;
            _httpContextAccesor = httpContextAccessor;
            _baseUrl = $"{config["CartUrl"]}/api/cart";
        }

        private async Task<string> GetUserTokenAsync()
        {
            var context = _httpContextAccesor.HttpContext;
            return await context.GetTokenAsync("access_token");
        }
        public Task AddItemToCart(ApplicationUser user, CartItem product)
        {
            throw new NotImplementedException();
        }

        public Task ClearCart(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<Cart> GetCart(ApplicationUser user)
        {
            var token = await GetUserTokenAsync();
            var getBasketUri = APIPaths.Basket.GetBasket(_baseUrl, user.Email);
            var dataString = await _apiClient.GetStringAsync(getBasketUri, token);
            var response = JsonConvert.DeserializeObject<Cart>(dataString) ??
                new Cart
                {
                    BuyerId = user.Email
                };
            return response;
        }

        public Task<Cart> SetQuantities(ApplicationUser user, Dictionary<string, int> quantities)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> UpdateCart(Cart Cart)
        {
            throw new NotImplementedException();
        }
    }
}
