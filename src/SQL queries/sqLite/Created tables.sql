-- Создание таблицы Category
CREATE TABLE Category (
    name VARCHAR(255) PRIMARY KEY
);

-- Создание таблицы Products
CREATE TABLE Products (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name VARCHAR(255) NOT NULL,
    description TEXT,
    purchase_price DECIMAL(10, 2),
    selling_price DECIMAL(10, 2),
    category VARCHAR(255)
);

-- Таблица для связи товаров и категорий (если нужно хранить список продуктов в категориях)
CREATE TABLE CategoryProduct (
    category_name VARCHAR(255),
    product_id INTEGER,
    PRIMARY KEY (category_name, product_id)
);

-- Создание таблицы Employees
CREATE TABLE Employees (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name VARCHAR(255) NOT NULL,
    position TEXT,
    salary DECIMAL(10, 2),
    hire_date DATE,
    moving_speed DECIMAL(5, 2),
    service_speed DECIMAL(5, 2)
);

-- Создание таблицы Store
CREATE TABLE Store (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name VARCHAR(255) NOT NULL,
    address TEXT,
    employee_id INTEGER,
    balance DECIMAL(15, 2) DEFAULT 0,
    total_earnings DECIMAL(15, 2) DEFAULT 0,
    total_expenses DECIMAL(15, 2) DEFAULT 0,
    total_products_sold INTEGER DEFAULT 0,
    total_customers INTEGER DEFAULT 0
);

-- Создание таблицы ProductStock для хранения информации о количестве продуктов на складе
CREATE TABLE ProductStock (
    product_id INTEGER PRIMARY KEY,
    quantity INTEGER NOT NULL
);

-- Создание таблицы ProductsToPurchase для хранения информации о продуктах, нужных для закупки
CREATE TABLE ProductsToPurchase (
    product_id INTEGER PRIMARY KEY,
    quantity INTEGER NOT NULL
);

-- Создание таблицы Transactions
CREATE TABLE Transactions (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    store_id INTEGER,
    transaction_datetime DATETIME NOT NULL,
    transaction_type VARCHAR(10) NOT NULL CHECK (transaction_type IN ('purchase', 'sale')),
    transaction_amount DECIMAL(15, 2) NOT NULL
);

-- Создание триггера для автоматического обновления списка товаров в категории при вставке нового товара
CREATE TRIGGER update_products_list_after_insert
AFTER INSERT ON Products
FOR EACH ROW
BEGIN
    INSERT OR IGNORE INTO CategoryProduct (category_name, product_id)
    VALUES (NEW.category, NEW.id);
END;
