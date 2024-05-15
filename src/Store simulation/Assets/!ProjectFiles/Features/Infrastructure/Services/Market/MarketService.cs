using System;
using Infrastructure.Services.DataBase;
using UniRx;
using UnityEngine;

namespace Infrastructure.Services.Market
{
    public class MarketService : IMarketService
    {
        public MarketService(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public MarketData MarketData { get; private set; }
        public Products Products { get; private set; }

        private readonly IDataBaseService _dataBaseService;

        public void InitializeData()
        {
            var storeData = _dataBaseService.GetStoreData(1);
            MarketData = new MarketData
            {
                balance = new IntReactiveProperty(storeData.Balance),
                earned = new IntReactiveProperty(storeData.TotalEarnings),
                spent = new IntReactiveProperty(storeData.TotalExpenses),
                productCount = new IntReactiveProperty(storeData.TotalProductsSold),
                buyerCount = new IntReactiveProperty(storeData.TotalCustomers)
            };

            var productDatas = _dataBaseService.GetAllProducts();
            Products = new Products(productDatas);
        }

        public void OrderProducts()
        {
            Debug.Log($"OrderProducts: {DateTime.Now}");

            var deliveryCost = 1000;
            var totalPrice = deliveryCost;

            var productsToPurchase = _dataBaseService.GetAllProductsToPurchase();

            Debug.Log($"productsToPurchase: {productsToPurchase.Length}");

            if (productsToPurchase.Length == 0)
            {
                return;
            }

            foreach (var product in productsToPurchase)
            {
                var productData = _dataBaseService.GetProductById(product.ProductId);
                var quantity = product.Quantity * 2;
                var price = productData.PurchasePrice * quantity;
                
                Debug.Log($"Product: {productData.Name}, Quantity: {quantity}, Price: {price}");

                if (totalPrice + price < MarketData.balance.Value)
                {
                    totalPrice += price;
                    
                    if (!_dataBaseService.CheckProductToStockById(product.ProductId))
                    {
                        _dataBaseService.AddProductToStockById(product.ProductId, quantity);
                    }
                    else
                    {
                        var productToStockCount = _dataBaseService.GetProductToStockById(product.ProductId).Quantity;
                        _dataBaseService.UpdateProductToStockById(product.ProductId, productToStockCount + quantity);
                    }

                    _dataBaseService.RemoveProductToPurchaseById(product.ProductId);
                }
                else
                {
                    var productToStockCount = _dataBaseService.GetProductToStockById(product.ProductId).Quantity;
                    var productToPurchaseCount = _dataBaseService.GetProductToPurchaseById(product.ProductId).Quantity;

                    for (var i = 0; i < quantity; i++)
                    {
                        if (totalPrice + productData.PurchasePrice < MarketData.balance.Value)
                        {
                            totalPrice += productData.PurchasePrice;

                            if (!_dataBaseService.CheckProductToStockById(product.ProductId))
                            {
                                _dataBaseService.AddProductToStockById(product.ProductId, 0);
                            }

                            var count = i + 1;
                            _dataBaseService.UpdateProductToStockById(product.ProductId, productToStockCount + count);
                            _dataBaseService.UpdateProductToPurchaseById(product.ProductId,
                                productToPurchaseCount - count);
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (_dataBaseService.GetProductToPurchaseById(product.ProductId).Quantity == 0)
                    {
                        _dataBaseService.RemoveProductToPurchaseById(product.ProductId);
                    }
                }

                _dataBaseService.AddTransaction(1, TransactionData.TransactionType.purchase, price);

                MarketData.spent.Value += price;
                MarketData.balance.Value -= price;

                UpdateStoreData();
            }
        }

        public void PurchaseByBuyer((int id, int count)[] cart)
        {
            var totalPrice = 0;

            foreach (var product in cart)
            {
                PurchaseByBuyer(product.id, product.count, out var price);
                totalPrice += price;
            }

            MarketData.buyerCount.Value++;

            _dataBaseService.AddTransaction(1, TransactionData.TransactionType.sale, totalPrice);

            UpdateStoreData();
        }

        private void PurchaseByBuyer(int id, int count, out int price)
        {
            var productData = _dataBaseService.GetProductById(id);
            var isProductToStock = _dataBaseService.CheckProductToStockById(id);

            if (isProductToStock == false)
            {
                var isProductToPurchase = _dataBaseService.CheckProductToPurchaseById(id);

                if (isProductToPurchase)
                {
                    _dataBaseService.UpdateProductToPurchaseById(id, count);
                }
                else
                {
                    _dataBaseService.AddProductToPurchaseById(id, count);
                }

                price = 0;

                return;
            }

            var countAvailable = _dataBaseService.GetProductToStockById(id).Quantity;

            if (count > countAvailable)
            {
                var purchaseQuantity = count - countAvailable;
                count = countAvailable;

                var isProductToPurchase = _dataBaseService.CheckProductToPurchaseById(id);

                if (isProductToPurchase)
                {
                    _dataBaseService.UpdateProductToPurchaseById(id, purchaseQuantity);
                }
                else
                {
                    _dataBaseService.AddProductToPurchaseById(id, purchaseQuantity);
                }
            }

            price = productData.SellingPrice * count;

            MarketData.productCount.Value += count;
            MarketData.balance.Value += price;
            MarketData.earned.Value += price;
        }

        private void UpdateStoreData()
        {
            _dataBaseService.UpdatedStoreData(1, MarketData.balance.Value, MarketData.earned.Value,
                MarketData.spent.Value, MarketData.productCount.Value, MarketData.buyerCount.Value);
        }
    }
}