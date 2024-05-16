using System;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using UnityEngine;

namespace Infrastructure.Services.DataBase
{
    public class SqLiteDataBaseService : IDataBaseService, IDisposable
    {
        private SqliteConnection _connection;

        public void ConnectToDataBase()
        {
            if (!File.Exists(Path.Combine(Application.streamingAssetsPath, "data_base.db")))
            {
                Debug.LogError("Файл базы данных не найден");
                return;
            }

            _connection = new SqliteConnection("URI=file:"
                                               + Application.streamingAssetsPath
                                               + "/data_base.db"
            );
            _connection.Open();
        }

        public CategoryData[] GetAllCategories()
        {
            var query = "SELECT name " +
                        "FROM Category";

            var dataTable = ExecuteQuery(query);

            var categoryData = new CategoryData[dataTable.Rows.Count];

            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                categoryData[i] = new CategoryData
                {
                    Name = dataTable.Rows[i]["name"].ToString()
                };
            }

            return categoryData;
        }

        public CategoryProductData[] GetAllCategoryProducts()
        {
            var query = "SELECT product_id, category_name " +
                        "FROM CategoryProduct";

            var dataTable = ExecuteQuery(query);

            var categoryProductData = new CategoryProductData[dataTable.Rows.Count];

            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                categoryProductData[i] = new CategoryProductData
                {
                    CategoryId = dataTable.Rows[i]["category_name"].ToString(),
                    ProductId = Convert.ToInt32(dataTable.Rows[i]["product_id"]),
                };
            }

            return categoryProductData;
        }

        public EmployeeData[] GetAllEmployees()
        {
            var query = "SELECT id, name, position, salary, hire_date, moving_speed, service_speed " +
                        "FROM Employees";

            var dataTable = ExecuteQuery(query);

            var employeeData = new EmployeeData[dataTable.Rows.Count];

            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                employeeData[i] = new EmployeeData
                {
                    Id = Convert.ToInt32(dataTable.Rows[i]["id"]),
                    Name = dataTable.Rows[i]["name"].ToString(),
                    Position = dataTable.Rows[i]["position"].ToString(),
                    Salary = Convert.ToInt32(dataTable.Rows[i]["salary"]),
                    HireDate = Convert.ToDateTime(dataTable.Rows[i]["hire_date"]),
                    MovingSpeed = Convert.ToInt32(dataTable.Rows[i]["moving_speed"]),
                    ServiceSpeed = Convert.ToInt32(dataTable.Rows[i]["service_speed"])
                };
            }

            return employeeData;
        }

        public StoreData[] GetAllStores()
        {
            var query =
                "SELECT id, name, address, employee_id, balance, total_earnings, total_expenses, total_products_sold, total_customers " +
                "FROM Store";

            var dataTable = ExecuteQuery(query);

            var storeData = new StoreData[dataTable.Rows.Count];

            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                storeData[i] = new StoreData
                {
                    Id = Convert.ToInt32(dataTable.Rows[i]["id"]),
                    Name = dataTable.Rows[i]["name"].ToString(),
                    Address = dataTable.Rows[i]["address"].ToString(),
                    EmployeeId = Convert.ToInt32(dataTable.Rows[i]["employee_id"]),
                    Balance = Convert.ToInt32(dataTable.Rows[i]["balance"]),
                    TotalEarnings = Convert.ToInt32(dataTable.Rows[i]["total_earnings"]),
                    TotalExpenses = Convert.ToInt32(dataTable.Rows[i]["total_expenses"]),
                    TotalProductsSold = Convert.ToInt32(dataTable.Rows[i]["total_products_sold"]),
                    TotalCustomers = Convert.ToInt32(dataTable.Rows[i]["total_customers"])
                };
            }

            return storeData;
        }

        public StoreData GetStoreData(int storeId)
        {
            var query =
                "SELECT id, name, address, employee_id, balance, total_earnings, total_expenses, total_products_sold, total_customers " +
                "FROM Store " +
                $"WHERE id = {storeId}";

            var dataTable = ExecuteQuery(query);

            var storeData = new StoreData
            {
                Id = Convert.ToInt32(dataTable.Rows[0]["id"]),
                Name = dataTable.Rows[0]["name"].ToString(),
                Address = dataTable.Rows[0]["address"].ToString(),
                EmployeeId = Convert.ToInt32(dataTable.Rows[0]["employee_id"]),
                Balance = Convert.ToInt32(dataTable.Rows[0]["balance"]),
                TotalEarnings = Convert.ToInt32(dataTable.Rows[0]["total_earnings"]),
                TotalExpenses = Convert.ToInt32(dataTable.Rows[0]["total_expenses"]),
                TotalProductsSold = Convert.ToInt32(dataTable.Rows[0]["total_products_sold"]),
                TotalCustomers = Convert.ToInt32(dataTable.Rows[0]["total_customers"])
            };

            return storeData;
        }

        public void UpdatedStoreData(int storeId, int balance, int totalEarnings, int totalExpenses,
            int totalProductsSold, int totalCustomers)
        {
            var query = "UPDATE Store " +
                        $"SET balance = {balance}, total_earnings = {totalEarnings}, total_expenses = {totalExpenses}, total_products_sold = {totalProductsSold}, total_customers = {totalCustomers} " +
                        $"WHERE id = {storeId}";
            ExecuteNonQuery(query);
        }

        public TransactionData[] GetAllTransactions()
        {
            var query = "SELECT id, store_id, transaction_datetime, transaction_type, transaction_amount " +
                        "FROM Transactions";

            var dataTable = ExecuteQuery(query);

            var transactionData = new TransactionData[dataTable.Rows.Count];

            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                transactionData[i] = new TransactionData
                {
                    Id = Convert.ToInt32(dataTable.Rows[i]["id"]),
                    StoreId = Convert.ToInt32(dataTable.Rows[i]["store_id"]),
                    TransactionDateTime = Convert.ToDateTime(dataTable.Rows[i]["transaction_datetime"]),
                    Type = (TransactionType)Enum.Parse(typeof(TransactionType),
                        dataTable.Rows[i]["transaction_type"].ToString()),
                    TransactionAmount = Convert.ToInt32(dataTable.Rows[i]["transaction_amount"])
                };
            }

            return transactionData;
        }

        public TransactionData GetTransactionById(int id)
        {
            var query = "SELECT id, store_id, transaction_datetime, transaction_type, transaction_amount " +
                        "FROM Transactions " +
                        $"WHERE id = {id}";

            var dataTable = ExecuteQuery(query);

            var transactionData = new TransactionData
            {
                Id = Convert.ToInt32(dataTable.Rows[0]["id"]),
                StoreId = Convert.ToInt32(dataTable.Rows[0]["store_id"]),
                TransactionDateTime = Convert.ToDateTime(dataTable.Rows[0]["transaction_datetime"]),
                Type = (TransactionType)Enum.Parse(typeof(TransactionType),
                    dataTable.Rows[0]["transaction_type"].ToString()),
                TransactionAmount = Convert.ToInt32(dataTable.Rows[0]["transaction_amount"])
            };

            return transactionData;
        }

        public EmployeeData GetEmployeeByStoreId(int storeId)
        {
            var query = "SELECT e.id, e.name, e.position, e.salary, e.hire_date, e.moving_speed, e.service_speed " +
                        "FROM Employees e " +
                        "JOIN Store s ON e.id = s.employee_id " +
                        $"WHERE s.id = {storeId}";

            var dataTable = ExecuteQuery(query);

            var employeeData = new EmployeeData
            {
                Id = Convert.ToInt32(dataTable.Rows[0]["id"]),
                Name = dataTable.Rows[0]["name"].ToString(),
                Position = dataTable.Rows[0]["position"].ToString(),
                Salary = Convert.ToInt32(dataTable.Rows[0]["salary"]),
                HireDate = Convert.ToDateTime(dataTable.Rows[0]["hire_date"]),
                MovingSpeed = Convert.ToInt32(dataTable.Rows[0]["moving_speed"]),
                ServiceSpeed = Convert.ToInt32(dataTable.Rows[0]["service_speed"])
            };

            return employeeData;
        }

        public ProductData[] GetAllProducts()
        {
            var query = "SELECT id, name, description, purchase_price, selling_price, category " +
                        "FROM Products";

            var dataTable = ExecuteQuery(query);

            var productData = new ProductData[dataTable.Rows.Count];

            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                productData[i] = new ProductData
                {
                    Id = Convert.ToInt32(dataTable.Rows[i]["id"]),
                    Name = dataTable.Rows[i]["name"].ToString(),
                    Description = dataTable.Rows[i]["description"].ToString(),
                    PurchasePrice = Convert.ToInt32(dataTable.Rows[i]["purchase_price"]),
                    SellingPrice = Convert.ToInt32(dataTable.Rows[i]["selling_price"]),
                    Category = dataTable.Rows[i]["category"].ToString()
                };
            }

            return productData;
        }

        public ProductData GetProductById(int id)
        {
            var query = "SELECT id, name, description, purchase_price, selling_price, category " +
                        "FROM Products " +
                        $"WHERE id = {id}";

            var dataTable = ExecuteQuery(query);

            var productData = new ProductData
            {
                Id = Convert.ToInt32(dataTable.Rows[0]["id"]),
                Name = dataTable.Rows[0]["name"].ToString(),
                Description = dataTable.Rows[0]["description"].ToString(),
                PurchasePrice = Convert.ToInt32(dataTable.Rows[0]["purchase_price"]),
                SellingPrice = Convert.ToInt32(dataTable.Rows[0]["selling_price"]),
                Category = dataTable.Rows[0]["category"].ToString()
            };

            return productData;
        }

        public ProductCountData[] GetAllProductToStock()
        {
            var query = "SELECT ps.product_id, p.name, ps.quantity " +
                        "FROM ProductStock ps " +
                        "JOIN Products p " +
                        "ON ps.product_id = p.id";

            var dataTable = ExecuteQuery(query);

            var productCountData = new ProductCountData[dataTable.Rows.Count];

            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                productCountData[i] = new ProductCountData
                {
                    ProductId = Convert.ToInt32(dataTable.Rows[i]["product_id"]),
                    ProductName = dataTable.Rows[i]["name"].ToString(),
                    Quantity = Convert.ToInt32(dataTable.Rows[i]["quantity"])
                };
            }

            return productCountData;
        }

        public ProductCountData GetProductToStockById(int id)
        {
            var query = "SELECT ps.product_id, p.name, ps.quantity " +
                        "FROM ProductStock ps " +
                        "JOIN Products p " +
                        "ON ps.product_id = p.id " +
                        $"WHERE ps.product_id = {id}";

            var dataTable = ExecuteQuery(query);

            var productCountData = new ProductCountData
            {
                ProductId = Convert.ToInt32(dataTable.Rows[0]["product_id"]),
                ProductName = dataTable.Rows[0]["name"].ToString(),
                Quantity = Convert.ToInt32(dataTable.Rows[0]["quantity"])
            };

            return productCountData;
        }

        public void UpdateProductToStockById(int id, int quantity)
        {
            var query = "UPDATE ProductStock " +
                        $"SET quantity = {quantity} " +
                        $"WHERE product_id = {id}";

            ExecuteNonQuery(query);
        }

        public bool CheckProductToStockById(int id)
        {
            var query = "SELECT * " +
                        "FROM ProductStock " +
                        $"WHERE product_id = {id}";

            var dataTable = ExecuteQuery(query);

            return dataTable.Rows.Count > 0;
        }

        public void AddProductToStockById(int id, int quantity)
        {
            var query = "INSERT INTO ProductStock (product_id, quantity) " +
                        $"VALUES ({id}, {quantity})";

            ExecuteNonQuery(query);
        }

        public void RemoveProductToStockById(int id)
        {
            var query = "DELETE FROM ProductStock " +
                        $"WHERE product_id = {id}";

            ExecuteNonQuery(query);
        }

        public ProductCountData[] GetAllProductsToPurchase()
        {
            var query = "SELECT ptp.product_id, p.name, ptp.quantity " +
                        "FROM ProductsToPurchase ptp " +
                        "JOIN Products p " +
                        "ON ptp.product_id = p.id";

            var dataTable = ExecuteQuery(query);

            var productCountData = new ProductCountData[dataTable.Rows.Count];

            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                productCountData[i] = new ProductCountData
                {
                    ProductId = Convert.ToInt32(dataTable.Rows[i]["product_id"]),
                    ProductName = dataTable.Rows[i]["name"].ToString(),
                    Quantity = Convert.ToInt32(dataTable.Rows[i]["quantity"])
                };
            }

            return productCountData;
        }

        public ProductCountData GetProductToPurchaseById(int id)
        {
            var query = "SELECT ptp.product_id, p.name, ptp.quantity " +
                        "FROM ProductsToPurchase ptp " +
                        "JOIN Products p " +
                        "ON ptp.product_id = p.id " +
                        $"WHERE ptp.product_id = {id}";

            var dataTable = ExecuteQuery(query);

            var productCountData = new ProductCountData
            {
                ProductId = Convert.ToInt32(dataTable.Rows[0]["product_id"]),
                ProductName = dataTable.Rows[0]["name"].ToString(),
                Quantity = Convert.ToInt32(dataTable.Rows[0]["quantity"])
            };

            return productCountData;
        }

        public void UpdateProductToPurchaseById(int id, int quantity)
        {
            var query = "UPDATE ProductsToPurchase " +
                        $"SET quantity = {quantity} " +
                        $"WHERE product_id = {id}";

            ExecuteNonQuery(query);
        }

        public bool CheckProductToPurchaseById(int id)
        {
            var query = "SELECT * " +
                        "FROM ProductsToPurchase " +
                        $"WHERE product_id = {id}";

            var dataTable = ExecuteQuery(query);

            return dataTable.Rows.Count > 0;
        }

        public void AddProductToPurchaseById(int id, int quantity)
        {
            var query = "INSERT INTO ProductsToPurchase (product_id, quantity) " +
                        $"VALUES ({id}, {quantity})";

            ExecuteNonQuery(query);
        }

        public void RemoveProductToPurchaseById(int id)
        {
            var query = "DELETE FROM ProductsToPurchase " +
                        $"WHERE product_id = {id}";

            ExecuteNonQuery(query);
        }

        public void AddTransaction(int storeId, TransactionType type, int totalPrice)
        {
            var query =
                "INSERT INTO Transactions (store_id, transaction_datetime, transaction_type, transaction_amount) " +
                $"VALUES ({storeId}, '{DateTime.Now:yyyy-MM-dd HH:mm:ss}', '{type}', {totalPrice})";

            ExecuteNonQuery(query);
        }

        private void ExecuteNonQuery(string query)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = query;
            command.ExecuteNonQuery();
        }

        private DataTable ExecuteQuery(string query)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = query;

            using var reader = command.ExecuteReader();

            var dataTable = new DataTable();

            try
            {
                dataTable.Load(reader);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

            return dataTable;
        }

        public void Dispose()
        {
            _connection?.Close();
        }
    }
}