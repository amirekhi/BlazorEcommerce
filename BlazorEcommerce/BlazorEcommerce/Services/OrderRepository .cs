using BlazorEcommerce.Data;
using BlazorEcommerce.Interfaces;
using ClassLibrary1.DTOs.Entities;
using ClassLibrary1.Entities;
using Microsoft.EntityFrameworkCore;

public class OrderRepository : IServerOrderRepository
{
    private readonly DataContext _context;

    public OrderRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateOrderAsync(string userId, CreateOrderDto dto)
    {
        var cart = await _context.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null || !cart.Items.Any())
            throw new InvalidOperationException("Cart is empty or does not exist.");


        var orderItems = cart.Items.Select(item => new OrderItem
        {
            ProductId = item.ProductId,
            ProductName = item.Product.Name,
            ProductImageUrl = item.Product.ImageUrl,
            UnitPrice = item.Product.Price,
            Quantity = item.Quantity
        }).ToList();

       var totalPrice = orderItems.Sum(i => i.UnitPrice * i.Quantity);

        var order = new Order
        {
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            ShippingAddress = dto.ShippingAddress,
            PaymentMethod = dto.PaymentMethod,
            Items = orderItems,
            TotalPrice = totalPrice,
            Status = "Delivered",
        };

        _context.Orders.Add(order);

        _context.CartItems.RemoveRange(cart.Items);
        _context.Carts.Remove(cart);

        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<List<Order>> GetOrdersForUserAsync(string userId)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(int orderId, string userId)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);
    }
}
