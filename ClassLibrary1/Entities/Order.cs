using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary1.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending"; // Could use enum instead
           // New fields
        public string ShippingAddress { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }

}