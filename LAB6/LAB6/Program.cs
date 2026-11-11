
using System;
using System.Collections.Generic;
using System.Linq;

namespace LAB6
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Product(int id, string name, decimal price, int quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }

    class Order
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }

        public Order(int id, Product product, int quantity)
        {
            Id = id;
            Product = product;
            Quantity = quantity;
            Total = product.Price * quantity;
        }

        public void ShowInformation()
        {
            Console.WriteLine("\n--- ІНФОРМАЦІЯ ПРО ЗАМОВЛЕННЯ ---");
            Console.WriteLine("Номер замовлення: " + Id);
            Console.WriteLine("Товар: " + Product.Name);
            Console.WriteLine("Кількість: " + Quantity);
            Console.WriteLine("Ціна за одиницю: " + Product.Price + " грн");
            Console.WriteLine("Загальна сума: " + Total + " грн");
        }
    }

    class StoreService
    {
        private List<Product> products;
        private List<Order> orders;

        public StoreService()
        {
            products = new List<Product>();
            orders = new List<Order>();

            products.Add(new Product(1, "Хліб", 25, 30));
            products.Add(new Product(2, "Молоко", 40, 20));
            products.Add(new Product(3, "Вода", 20, 50));
            products.Add(new Product(4, "Навушники", 500, 10));
            products.Add(new Product(5, "Шоколад", 35, 15));
        }

        public void ShowProducts()
        {
            Console.WriteLine("\n--- СПИСОК ТОВАРІВ ---");

            foreach (Product product in products)
            {
                Console.WriteLine(
                    product.Id + ". " + product.Name +
                    " | Ціна: " + product.Price +
                    " грн | Кількість: " + product.Quantity);
            }
        }

        public void CreateOrder(int productId, int quantity)
        {
            Product product = products.FirstOrDefault(
                p => p.Id == productId);

            if (product == null)
            {
                Console.WriteLine("Помилка: товар не знайдено.");
                return;
            }

            if (quantity <= 0)
            {
                Console.WriteLine("Помилка: кількість повинна бути більшою за нуль.");
                return;
            }

            if (product.Quantity < quantity)
            {
                Console.WriteLine("Помилка: недостатня кількість товару.");
                Console.WriteLine("Доступно: " + product.Quantity);
                return;
            }

            product.Quantity -= quantity;

            int orderId = orders.Count + 1;

            Order order = new Order(orderId, product, quantity);

            orders.Add(order);

            Console.WriteLine("\nЗамовлення успішно створено!");

            order.ShowInformation();
        }

        public void ShowOrders()
        {
            Console.WriteLine("\n--- СПИСОК ЗАМОВЛЕНЬ ---");

            if (orders.Count == 0)
            {
                Console.WriteLine("Замовлень поки немає.");
                return;
            }

            foreach (Order order in orders)
            {
                order.ShowInformation();
            }
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
                Console.WriteLine("\n===== СИСТЕМА АВТОМАТИЗАЦІЇ МАГАЗИНУ =====");
                Console.WriteLine("1. Показати товари");
                Console.WriteLine("2. Оформити замовлення");
                Console.WriteLine("3. Переглянути замовлення");
                Console.WriteLine("0. Вихід");

                Console.Write("\nВаш вибір: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        store.ShowProducts();
                        break;

                    case "2":
                        store.ShowProducts();

                        Console.Write("\nВведіть ID товару: ");
                        int productId;

                        if (!int.TryParse(Console.ReadLine(), out productId))
                        {
                            Console.WriteLine("Некоректний ID товару.");
                            break;
                        }

                        Console.Write("Введіть кількість: ");
                        int quantity;

                        if (!int.TryParse(Console.ReadLine(), out quantity))
                        {
                            Console.WriteLine("Некоректна кількість.");
                            break;
                        }

                        store.CreateOrder(productId, quantity);
                        break;

                    case "3":
                        store.ShowOrders();
                        break;

                    case "0":
                        running = false;
                        Console.WriteLine("Програму завершено.");
                        break;

                    default:
                        Console.WriteLine("Невірна команда.");
                        break;
                }
            }
        }
    }
}
