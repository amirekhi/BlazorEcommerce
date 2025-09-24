using System.Linq;
using System.Threading.Tasks;
using BlazorEcommerce.Extensions;
using BlazorEcommerce.Interfaces;
using BlazorEcommerce.Mappers;
using ClassLibrary1.DTOs.Entities;
using ClassLibrary1.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IServerCartRepository _cartRepo;
        private readonly UserManager<User> _userManager;

        public CartController(IServerCartRepository cartRepo, UserManager<User> userManager)
        {
            _cartRepo = cartRepo;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<CartDto>> GetCart()
        {
            var user = await GetCurrentUserAsync();
            var cart = await _cartRepo.GetCartAsync(user.Id);
            if (cart == null)
            {
                return Ok(new CartDto
                {
                    Items = new List<CartItemDto>()  // Return an empty list instead of 404 or null
                });
            }

            return Ok(cart.ToCartDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CartDto>> AddItem([FromBody] AddCartItemDto dto)
        {
            var user = await GetCurrentUserAsync();
            var cart = await _cartRepo.AddItemAsync(user.Id, dto.ProductId, dto.Quantity);
            return Ok(cart.ToCartDto());
        }

        [HttpPut("items/{productId}")]
        [Authorize]
        public async Task<ActionResult<CartDto>> UpdateItem(int productId, [FromBody] int quantity)
        {
            var user = await GetCurrentUserAsync();
            var cart = await _cartRepo.UpdateItemQuantityAsync(user.Id, productId, quantity);
            return Ok(cart.ToCartDto());
        }

        [HttpDelete("items/{productId}")]
        [Authorize]
        public async Task<ActionResult<CartDto>> RemoveItem(int productId)
        {
            var user = await GetCurrentUserAsync();
            var cart = await _cartRepo.RemoveItemAsync(user.Id, productId);
            return Ok(cart.ToCartDto());
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<bool>> ClearCart()
        {
            var user = await GetCurrentUserAsync();
            var result = await _cartRepo.ClearCartAsync(user.Id);
            return Ok(result);
        }

        private async Task<User> GetCurrentUserAsync()
        {
            var userName = User.GetUserName();
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                throw new UnauthorizedAccessException("User not found.");
            return user;
        }
    }
}
