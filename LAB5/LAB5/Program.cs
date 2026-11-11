
using System;

namespace LAB5
{
    enum OrderState
    {
        Created,
        WaitingForPayment,
        Paid,
        Preparing,
        Ready,
        Completed,
        Cancelled
    }

    class StoreOrder
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public OrderState State { get; private set; }

        public StoreOrder(int id, string customerName)
        {
            Id = id;
            CustomerName = customerName;
            State = OrderState.Created;
        }

        public void ConfirmOrder()
        {
            if (State == OrderState.Created)
            {
                State = OrderState.WaitingForPayment;
                Console.WriteLine("Замовлення підтверджено.");
            }
            else
            {
                InvalidTransition();
            }
        }

        public void ConfirmPayment()
        {
            if (State == OrderState.WaitingForPayment)
            {
                State = OrderState.Paid;
                Console.WriteLine("Оплату підтверджено.");
            }
            else
            {
                InvalidTransition();
            }
        }

        public void StartPreparing()
        {
            if (State == OrderState.Paid)
            {
                State = OrderState.Preparing;
                Console.WriteLine("Почато підготовку замовлення.");
            }
            else
            {
                InvalidTransition();
            }
        }

        public void FinishPreparing()
        {
            if (State == OrderState.Preparing)
            {
                State = OrderState.Ready;
                Console.WriteLine("Замовлення готове до видачі.");
            }
            else
            {
                InvalidTransition();
            }
        }

        public void CompleteOrder()
        {
            if (State == OrderState.Ready)
            {
                State = OrderState.Completed;
                Console.WriteLine("Замовлення видано покупцю.");
            }
            else
            {
                InvalidTransition();
            }
        }

        public void CancelOrder()
        {
            if (State == OrderState.Created ||
                State == OrderState.WaitingForPayment ||
                State == OrderState.Paid)
            {
                State = OrderState.Cancelled;
                Console.WriteLine("Замовлення скасовано.");
            }
            else
            {
                InvalidTransition();
            }
        }

        public void ShowInformation()
        {
            Console.WriteLine("\n--- ІНФОРМАЦІЯ ПРО ЗАМОВЛЕННЯ ---");
            Console.WriteLine("Номер: " + Id);
            Console.WriteLine("Покупець: " + CustomerName);
            Console.WriteLine("Стан: " + GetStateName());
        }

        private string GetStateName()
        {
            switch (State)
            {
                case OrderState.Created:
                    return "Створене";

                case OrderState.WaitingForPayment:
                    return "Очікує оплати";

                case OrderState.Paid:
                    return "Оплачене";

                case OrderState.Preparing:
                    return "Готується";

                case OrderState.Ready:
                    return "Готове";

                case OrderState.Completed:
                    return "Завершене";

                case OrderState.Cancelled:
                    return "Скасоване";

                default:
                    return "Невідомий стан";
            }
        }

        private void InvalidTransition()
        {
            Console.WriteLine(
                "Помилка: перехід із поточного стану неможливий.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введіть ім'я покупця: ");
            string name = Console.ReadLine();

            StoreOrder order = new StoreOrder(1, name);

            bool running = true;

            while (running)
            {
                Console.WriteLine("\n===== СИСТЕМА АВТОМАТИЗАЦІЇ МАГАЗИНУ =====");
                Console.WriteLine("1. Підтвердити замовлення");
                Console.WriteLine("2. Підтвердити оплату");
                Console.WriteLine("3. Почати підготовку");
                Console.WriteLine("4. Завершити підготовку");
                Console.WriteLine("5. Видати замовлення");
                Console.WriteLine("6. Скасувати замовлення");
                Console.WriteLine("7. Переглянути замовлення");
                Console.WriteLine("0. Вихід");
                Console.Write("\nВаш вибір: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        order.ConfirmOrder();
                        break;

                    case "2":
                        order.ConfirmPayment();
                        break;

                    case "3":
                        order.StartPreparing();
                        break;

                    case "4":
                        order.FinishPreparing();
                        break;

                    case "5":
                        order.CompleteOrder();
                        break;

                    case "6":
                        order.CancelOrder();
                        break;

                    case "7":
                        order.ShowInformation();
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
