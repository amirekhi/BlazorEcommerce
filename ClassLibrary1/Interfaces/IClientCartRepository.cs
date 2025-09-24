using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary1.DTOs.Entities;

namespace ClassLibrary1.Interfaces
{
 public interface IClientCartRepository
    {
        Task<CartDto?> GetCartAsync();
        Task<CartDto?> AddItemToCartAsync(AddCartItemDto item);
        Task<CartDto?> RemoveItemFromCartAsync(int productId);
        Task<CartDto?> UpdateItemQuantityAsync(int productId, int quantity);
        Task<bool> ClearCartAsync();
    }
}