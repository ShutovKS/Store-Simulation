-- Удаление триггера (MySQL)
DROP TRIGGER IF EXISTS update_products_list_after_insert;

-- Удаление таблиц
DROP TABLE IF EXISTS Transactions;
DROP TABLE IF EXISTS ProductsToPurchase;
DROP TABLE IF EXISTS ProductStock;
DROP TABLE IF EXISTS Store;
DROP TABLE IF EXISTS Employees;
DROP TABLE IF EXISTS CategoryProduct;
DROP TABLE IF EXISTS Products;
DROP TABLE IF EXISTS Category;
