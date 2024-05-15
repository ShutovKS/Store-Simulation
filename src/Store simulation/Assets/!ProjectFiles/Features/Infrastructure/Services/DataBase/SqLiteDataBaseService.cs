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

        public StoreData GetStoreData(int storeId)
        {
            var query = $"SELECT id, name, address, employee_id, balance, total_earnings, total_expenses, total_products_sold, total_customers FROM Store WHERE id = {storeId}";

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
            var query = $"UPDATE Store SET balance = {balance}, total_earnings = {totalEarnings}, total_expenses = {totalExpenses}, total_products_sold = {totalProductsSold}, total_customers = {totalCustomers} WHERE id = {storeId}";
            ExecuteNonQuery(query);
        }

        public void ConnectToDataBase()
        {
            if (!File.Exists(Path.Combine(Application.streamingAssetsPath, "data_base.db")))
            {
                Debug.LogError("Файл базы данных не найден");
                return;
            }

            _connection = new SqliteConnection("URI=file:" + Application.streamingAssetsPath + "/data_base.db");
            _connection.Open();
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

            dataTable.Load(reader);

            return dataTable;
        }

        public void Dispose()
        {
            _connection?.Close();
        }
    }

    public struct StoreData
    {
        public int Id;
        public string Name;
        public string Address;
        public int EmployeeId;
        public int Balance;
        public int TotalEarnings;
        public int TotalExpenses;
        public int TotalProductsSold;
        public int TotalCustomers;
    }
}