-- Создание таблицы Category
CREATE TABLE Category (
    name VARCHAR(255) PRIMARY KEY,
    products_list TEXT
);

-- Создание таблицы Products
CREATE TABLE Products (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    description TEXT,
    purchase_price DECIMAL(10, 2),
	selling_price DECIMAL(10, 2);
	category VARCHAR(255),
    FOREIGN KEY (category) REFERENCES Category(name)
);

-- Создание триггера для автоматического обновления списка товаров в категории при вставке нового товара
DELIMITER //

CREATE TRIGGER update_products_list_after_insert
AFTER INSERT ON Products
FOR EACH ROW
BEGIN
    DECLARE new_product_list TEXT;
    
    -- Получение текущего списка товаров для категории
    SELECT GROUP_CONCAT(name ORDER BY name SEPARATOR ', ')
    INTO new_product_list
    FROM Products
    WHERE category = NEW.category;
    
    -- Обновление списка товаров в категории
    UPDATE Category
    SET products_list = new_product_list
    WHERE name = NEW.category;
END//

DELIMITER ;

-- Создание таблицы Employees
CREATE TABLE Employees (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    position TEXT,
    salary DECIMAL(10, 2),
    hire_date DATE,
    moving_speed DECIMAL(5, 2),
    service_speed DECIMAL(5, 2)
);

-- Создание таблицы Store
CREATE TABLE Store (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    address TEXT,
    employee_id INT,
    balance DECIMAL(15, 2) DEFAULT 0,
    total_earnings DECIMAL(15, 2) DEFAULT 0,
    total_expenses DECIMAL(15, 2) DEFAULT 0,
    total_products_sold INT DEFAULT 0,
    total_customers INT DEFAULT 0
);

-- Создание таблицы ProductStock для хранения информации о количестве продуктов на складе
CREATE TABLE ProductStock (
    product_id INT PRIMARY KEY,
    quantity INT NOT NULL
);

-- Создание таблицы ProductsToPurchase для хранения информации о продуктах, нужных для закупки
CREATE TABLE ProductsToPurchase (
    product_id INT PRIMARY KEY,
    quantity INT NOT NULL
);

-- Создание таблицы Transactions
CREATE TABLE Transactions (
    id INT AUTO_INCREMENT PRIMARY KEY,
    store_id INT,
    transaction_datetime DATETIME NOT NULL,
    transaction_type ENUM('purchase', 'sale') NOT NULL,
    products_list TEXT NOT NULL,
    transaction_amount DECIMAL(15, 2) NOT NULL,
    FOREIGN KEY (store_id) REFERENCES Store(id)
);
