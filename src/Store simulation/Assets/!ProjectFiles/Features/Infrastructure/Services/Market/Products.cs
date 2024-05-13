using System.Collections.Generic;

namespace Infrastructure.Services.Market
{
    public class Products
    {
        private readonly Dictionary<int, int> _products = new()
        {
            { 0, 999 },
        };

        public bool CheckProduct(int id, out int countAvailable)
        {
            return _products.TryGetValue(id, out countAvailable);
        }

        public bool CheckProduct(int id, int count)
        {
            return _products.TryGetValue(id, out var value) && value >= count;
        }

        public void AddProduct(int id, int count)
        {
            if (!_products.TryAdd(id, count))
            {
                _products[id] += count;
            }
        }

        public bool TryRemoveProduct(int id, int count)
        {
            if (!_products.TryGetValue(id, out var value))
            {
                return false;
            }

            if (value < count)
            {
                return false;
            }

            _products[id] -= count;
            return true;
        }
    }
}