using BlazorEcommerce.Data;
using BlazorEcommerce.Interfaces;
using ClassLibrary1.Entities;
using Microsoft.EntityFrameworkCore;

public class EfCartRepository : IServerCartRepository
{
    private readonly DataContext _db;

    public EfCartRepository(DataContext db)
    {
        _db = db;
    }

    public async Task<Cart> GetCartAsync(string userId)
    {
        return await _db.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

 public async Task<Cart> AddItemAsync(string userId, int productId, int quantity)
{
    var cart = await _db.Carts
        .Include(c => c.Items)
        .ThenInclude(i => i.Product) // <-- include products here
        .FirstOrDefaultAsync(c => c.UserId == userId);

    if (cart == null)
    {
        cart = new Cart { UserId = userId };
        _db.Carts.Add(cart);
    }

    var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
    if (item != null)
        item.Quantity += quantity;
    else
        cart.Items.Add(new CartItem { ProductId = productId, Quantity = quantity });

    await _db.SaveChangesAsync();

    // Reload cart with included products after changes (optional but safer)
    cart = await _db.Carts
        .Include(c => c.Items)
        .ThenInclude(i => i.Product)
        .FirstOrDefaultAsync(c => c.UserId == userId);

    return cart!;
}


    public async Task<Cart> RemoveItemAsync(string userId, int productId)
    {
        var cart = await _db.Carts.Include(c => c.Items).ThenInclude(i => i.Product).FirstOrDefaultAsync(c => c.UserId == userId);
        if (cart == null)
            return null;

        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            cart.Items.Remove(item);
            await _db.SaveChangesAsync();
        }

        return cart;
    }

    public async Task<Cart> UpdateItemQuantityAsync(string userId, int productId, int quantity)
    {
        var cart = await _db.Carts
            .Include(c => c.Items)
                .ThenInclude(i => i.Product) // <-- Add this line
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
            return null;

        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            item.Quantity = quantity;
            await _db.SaveChangesAsync();
        }

        return cart;
    }


    public async Task<bool> ClearCartAsync(string userId)
    {
        var cart = await _db.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.UserId == userId);
        if (cart == null)
            return false;

        _db.CartItems.RemoveRange(cart.Items);
        await _db.SaveChangesAsync();
        return true;
    }


}
