-- Удаление данных из таблицы Transactions
DELETE FROM Transactions;

-- Удаление данных из таблицы ProductsToPurchase
DELETE FROM ProductsToPurchase;

-- Удаление данных из таблицы ProductStock
DELETE FROM ProductStock;

-- Удаление данных из таблицы Store
DELETE FROM Store;

-- Удаление данных из таблицы Employees
DELETE FROM Employees;

-- Удаление данных из таблицы CategoryProduct
DELETE FROM CategoryProduct;

-- Удаление данных из таблицы Products
DELETE FROM Products;

-- Удаление данных из таблицы Category
DELETE FROM Category;

-- Восстановление структуры базы данных для сброса автоинкремента
VACUUM;
