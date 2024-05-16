using Infrastructure.Services.DataBase;

namespace Infrastructure.Services.Market
{
    public class Products
    {
        public Products(ProductData[] productDatas)
        {
            _productDatas = productDatas;
        }

        private readonly ProductData[] _productDatas;

        public ProductData GetRandomProduct()
        {
            var randomIndex = UnityEngine.Random.Range(0, _productDatas.Length);
            return _productDatas[randomIndex];
        }
    }
}