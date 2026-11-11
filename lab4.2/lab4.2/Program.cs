
using System;
using System.Collections.Generic;
using System.Linq;

namespace LAB4
{
    class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public Supplier(int id, string name, string phone)
        {
            Id = id;
            Name = name;
            Phone = phone;
        }
    }

    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; }
        public Supplier Supplier { get; set; }

        public Product(int id, string name, decimal price,
            int quantity, Category category, Supplier supplier)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
            Category = category;
            Supplier = supplier;
        }
    }

    class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Customer(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    class OrderItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public OrderItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public decimal Total
        {
            get { return Product.Price * Quantity; }
        }
    }

    class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public List<OrderItem> Items { get; set; }

        public Order(int id, Customer customer)
        {
            Id = id;
            Customer = customer;
            Items = new List<OrderItem>();
        }

        public decimal Total
        {
            get { return Items.Sum(item => item.Total); }
        }
    }

    class StoreService
    {
        private List<Category> categories;
        private List<Supplier> suppliers;
        private List<Product> products;
        private List<Customer> customers;
        private List<Order> orders;

        public StoreService()
        {
            categories = new List<Category>();
            suppliers = new List<Supplier>();
            products = new List<Product>();
            customers = new List<Customer>();
            orders = new List<Order>();

            SeedData();
        }

        private void SeedData()
        {
            Category food = new Category(1, "Продукти");
            Category drinks = new Category(2, "Напої");
            Category electronics = new Category(3, "Електроніка");

            categories.Add(food);
            categories.Add(drinks);
            categories.Add(electronics);

            Supplier supplier1 = new Supplier(1, "ТОВ Продукт", "0991112233");
            Supplier supplier2 = new Supplier(2, "ТехноСвіт", "0672223344");

            suppliers.Add(supplier1);
            suppliers.Add(supplier2);

            products.Add(new Product(1, "Хліб", 25, 30, food, supplier1));
            products.Add(new Product(2, "Молоко", 40, 20, food, supplier1));
            products.Add(new Product(3, "Вода", 20, 50, drinks, supplier1));
            products.Add(new Product(4, "Навушники", 500, 10, electronics, supplier2));

            Customer customer = new Customer(1, "Роман");
            customers.Add(customer);

            Order order = new Order(1, customer);
            order.Items.Add(new OrderItem(products[0], 2));
            order.Items.Add(new OrderItem(products[1], 1));

            orders.Add(order);
        }

        public void ShowAllProducts()
        {
            Console.WriteLine("\n--- ВСІ ТОВАРИ ---");

            foreach (Product product in products)
            {
                PrintProduct(product);
            }
        }

        public void SearchProductsByCategory()
        {
            ShowCategories();

            Console.Write("\nВведіть назву категорії: ");
            string name = Console.ReadLine();

            List<Product> result = products
                .Where(p => p.Category.Name.ToLower() == name.ToLower())
                .ToList();

            if (result.Count == 0)
            {
                Console.WriteLine("Товарів у цій категорії не знайдено.");
                return;
            }

            foreach (Product product in result)
            {
                PrintProduct(product);
            }
        }

        public void SearchProductByName()
        {
            Console.Write("\nВведіть назву товару: ");
            string name = Console.ReadLine();

            List<Product> result = products
                .Where(p => p.Name.ToLower().Contains(name.ToLower()))
                .ToList();

            if (result.Count == 0)
            {
                Console.WriteLine("Товар не знайдено.");
                return;
            }

            foreach (Product product in result)
            {
                PrintProduct(product);
            }
        }

        public void ShowOrders()
        {
            Console.WriteLine("\n--- ЗАМОВЛЕННЯ ---");

            foreach (Order order in orders)
            {
                Console.WriteLine("Замовлення №" + order.Id);
                Console.WriteLine("Покупець: " + order.Customer.Name);

                foreach (OrderItem item in order.Items)
                {
                    Console.WriteLine(item.Product.Name +
                        " x " + item.Quantity +
                        " = " + item.Total + " грн");
                }

                Console.WriteLine("Загальна сума: " + order.Total + " грн");
                Console.WriteLine();
            }
        }

        public void ShowStoreInformation()
        {
            Console.WriteLine("\n--- ІНФОРМАЦІЯ ПРО МАГАЗИН ---");
            Console.WriteLine("Кількість категорій: " + categories.Count);
            Console.WriteLine("Кількість постачальників: " + suppliers.Count);
            Console.WriteLine("Кількість товарів: " + products.Count);
            Console.WriteLine("Кількість покупців: " + customers.Count);
            Console.WriteLine("Кількість замовлень: " + orders.Count);
        }

        public void AddProduct()
        {
            Console.Write("\nНазва товару: ");
            string name = Console.ReadLine();

            Console.Write("Ціна: ");
            decimal price;

            if (!decimal.TryParse(Console.ReadLine(), out price) || price < 0)
            {
                Console.WriteLine("Некоректна ціна.");
                return;
            }

            Console.Write("Кількість: ");
            int quantity;

            if (!int.TryParse(Console.ReadLine(), out quantity) || quantity < 0)
            {
                Console.WriteLine("Некоректна кількість.");
                return;
            }

            ShowCategories();
            Console.Write("Введіть ID категорії: ");

            int categoryId;
            if (!int.TryParse(Console.ReadLine(), out categoryId))
            {
                Console.WriteLine("Некоректний ID.");
                return;
            }

            Category category = categories.FirstOrDefault(c => c.Id == categoryId);

            if (category == null)
            {
                Console.WriteLine("Категорію не знайдено.");
                return;
            }

            ShowSuppliers();
            Console.Write("Введіть ID постачальника: ");

            int supplierId;
            if (!int.TryParse(Console.ReadLine(), out supplierId))
            {
                Console.WriteLine("Некоректний ID.");
                return;
            }

            Supplier supplier = suppliers.FirstOrDefault(s => s.Id == supplierId);

            if (supplier == null)
            {
                Console.WriteLine("Постачальника не знайдено.");
                return;
            }

            int newId = products.Count == 0
                ? 1
                : products.Max(p => p.Id) + 1;

            Product product = new Product(
                newId, name, price, quantity, category, supplier);

            products.Add(product);

            Console.WriteLine("Товар успішно додано!");
        }

        public void ShowCategories()
        {
            Console.WriteLine("\n--- КАТЕГОРІЇ ---");

            foreach (Category category in categories)
            {
                Console.WriteLine(category.Id + ". " + category.Name);
            }
        }

        public void ShowSuppliers()
        {
            Console.WriteLine("\n--- ПОСТАЧАЛЬНИКИ ---");

            foreach (Supplier supplier in suppliers)
            {
                Console.WriteLine(
                    supplier.Id + ". " + supplier.Name +
                    " | Телефон: " + supplier.Phone);
            }
        }

        private void PrintProduct(Product product)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("ID: " + product.Id);
            Console.WriteLine("Назва: " + product.Name);
            Console.WriteLine("Ціна: " + product.Price + " грн");
            Console.WriteLine("Кількість: " + product.Quantity);
            Console.WriteLine("Категорія: " + product.Category.Name);
            Console.WriteLine("Постачальник: " + product.Supplier.Name);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            StoreService store = new StoreService();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n===== АВТОМАТИЗАЦІЯ МАГАЗИНУ =====");
                Console.WriteLine("1. Показати всі товари");
                Console.WriteLine("2. Пошук товарів за категорією");
                Console.WriteLine("3. Пошук товару за назвою");
                Console.WriteLine("4. Показати замовлення");
                Console.WriteLine("5. Інформація про магазин");
                Console.WriteLine("6. Додати товар");
                Console.WriteLine("7. Показати категорії");
                Console.WriteLine("8. Показати постачальників");
                Console.WriteLine("0. Вихід");
                Console.Write("\nВаш вибір: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        store.ShowAllProducts();
                        break;

                    case "2":
                        store.SearchProductsByCategory();
                        break;

                    case "3":
                        store.SearchProductByName();
                        break;

                    case "4":
                        store.ShowOrders();
                        break;

                    case "5":
                        store.ShowStoreInformation();
                        break;

                    case "6":
                        store.AddProduct();
                        break;

                    case "7":
                        store.ShowCategories();
                        break;

                    case "8":
                        store.ShowSuppliers();
                        break;

                    case "0":
                        running = false;
                        Console.WriteLine("Програму завершено.");
                        break;

                    default:
                        Console.WriteLine("Невірна команда. Спробуйте ще раз.");
                        break;
                }
            }
        }
    }
}
