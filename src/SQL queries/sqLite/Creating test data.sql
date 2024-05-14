-- Вставка тестовых данных в таблицу Category
INSERT INTO Category (name) VALUES
('Electronics'),
('Books'),
('Clothing');

-- Вставка тестовых данных в таблицу Products
INSERT INTO Products (name, description, purchase_price, selling_price, category) VALUES
('Smartphone', 'A high-end smartphone with a large display.', 500.00, 700.00, 'Electronics'),
('Laptop', 'A powerful laptop for professional use.', 800.00, 1200.00, 'Electronics'),
('Tablet', 'A versatile tablet for entertainment and productivity.', 300.00, 450.00, 'Electronics'),
('Fiction Book', 'A best-selling fiction book.', 10.00, 15.00, 'Books'),
('Non-fiction Book', 'A popular non-fiction book.', 12.00, 18.00, 'Books'),
('T-Shirt', 'A comfortable cotton t-shirt.', 5.00, 10.00, 'Clothing'),
('Jeans', 'Stylish and durable jeans.', 20.00, 40.00, 'Clothing');

-- Вставка тестовых данных в таблицу Employees
INSERT INTO Employees (name, position, salary, hire_date, moving_speed, service_speed) VALUES
('Alice', 'Manager', 5000.00, '2022-01-15', 1.2, 1.5),
('Bob', 'Salesperson', 3000.00, '2022-02-20', 1.0, 1.8),
('Charlie', 'Cashier', 2500.00, '2023-03-10', 0.8, 1.6);

-- Вставка тестовых данных в таблицу Store
INSERT INTO Store (name, address, employee_id, balance, total_earnings, total_expenses, total_products_sold, total_customers) VALUES
('Main Street Store', '123 Main St, Anytown', 1, 1000000.00, 20000.00, 5000.00, 150, 100),

-- Вставка тестовых данных в таблицу ProductStock
INSERT INTO ProductStock (product_id, quantity) VALUES
(1, 50),
(2, 30),
(3, 20),
(4, 100),
(5, 80),
(6, 200),
(7, 150);

-- Вставка тестовых данных в таблицу ProductsToPurchase
INSERT INTO ProductsToPurchase (product_id, quantity) VALUES
(1, 20),
(2, 15),
(3, 10),
(4, 50),
(5, 40),
(6, 100),
(7, 80);

-- Вставка тестовых данных в таблицу Transactions
INSERT INTO Transactions (store_id, transaction_datetime, transaction_type, transaction_amount) VALUES
(1, '2023-04-01 10:00:00', 'sale', 700.00),
(1, '2023-04-01 11:00:00', 'purchase', 500.00),
(2, '2023-04-01 12:00:00', 'sale', 1200.00),
(2, '2023-04-01 13:00:00', 'purchase', 800.00);

-- Вставка тестовых данных в таблицу TransactionProducts
INSERT INTO TransactionProducts (transaction_id, product_id, quantity) VALUES
(1, 1, 1),
(2, 1, 1),
(3, 2, 1),
(4, 2, 1);