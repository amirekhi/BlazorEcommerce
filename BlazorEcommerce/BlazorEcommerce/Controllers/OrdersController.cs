using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BlazorEcommerce.Extensions;
using BlazorEcommerce.Interfaces;
using ClassLibrary1.DTOs.Entities;
using ClassLibrary1.Entities;
using BlazorEcommerce.Mappers;

namespace BlazorEcommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IServerOrderRepository _orderRepo;

        public OrdersController(UserManager<User> userManager, IServerOrderRepository orderRepo)
        {
            _userManager = userManager;
            _orderRepo = orderRepo;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] CreateOrderDto dto)
        {
            var userName = User.GetUserName();
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null) return Unauthorized();

            var order = await _orderRepo.CreateOrderAsync(user.Id, dto);
            return Ok(order.ToOrderDto());
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<OrderDto>>> GetOrders()
        {
            var userName = User.GetUserName();
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null) return Unauthorized();

            var orders = await _orderRepo.GetOrdersForUserAsync(user.Id);
            return Ok(orders.Select(o => o.ToOrderDto()).ToList());
        }

        [HttpGet("{orderId}")]
        [Authorize]
        public async Task<ActionResult<OrderDto>> GetOrderById(int orderId)
        {
            var userName = User.GetUserName();
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null) return Unauthorized();

            var order = await _orderRepo.GetOrderByIdAsync( orderId , user.Id);

            if (order == null) return NotFound();

            return Ok(order.ToOrderDto());
        }
    }
}
