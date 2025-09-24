using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEcommerce.Client.Services
{
    public class CartStateService
    {
        public int ItemCount { get; private set; } = 0;

        public event Action? OnChange;

        public void SetItemCount(int count)
        {
            ItemCount = count;
            NotifyStateChanged();
        }

        public void IncrementItemCount()
        {
            ItemCount++;
            NotifyStateChanged();
        }

        public void DecrementItemCount()
        {
            if (ItemCount > 0)
            {
                ItemCount--;
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

}