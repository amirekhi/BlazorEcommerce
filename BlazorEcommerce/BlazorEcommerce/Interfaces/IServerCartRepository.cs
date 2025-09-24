using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary1.Entities;

namespace BlazorEcommerce.Interfaces
{
  public interface IServerCartRepository
{
    Task<Cart> GetCartAsync(string userId);
    Task<Cart> AddItemAsync(string userId, int productId, int quantity);
    Task<Cart> RemoveItemAsync(string userId, int productId);
    Task<Cart> UpdateItemQuantityAsync(string userId, int productId, int quantity);
    Task<bool> ClearCartAsync(string userId);
}
}