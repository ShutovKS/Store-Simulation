using System.Collections.Generic;

namespace Market
{
    public class Products
    {
        private readonly Dictionary<int, int> _products = new()
        {
            { 0, 999 },
        };

        public bool CheckProduct(int id)
        {
            return _products.TryGetValue(id, out var value) && value > 0;
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