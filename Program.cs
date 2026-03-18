using System;

namespace BankManagementSystem
{
    class Program
    {
        static void Main()
        {
            Bank bank = new Bank();
            bool exit = false;

            Console.WriteLine("=== Консольный Банк v3.0 ===");

            while (!exit)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1. Регистрация клиента");
                Console.WriteLine("2. Открыть счет");
                Console.WriteLine("3. Вход в систему");
                Console.WriteLine("4. Проверить сумму операции");
                Console.WriteLine("5. Отправить уведомление");
                Console.WriteLine("0. Выход");
                Console.Write("> ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Введите ФИО: ");
                        string name = Console.ReadLine();
                        Console.Write("Введите возраст: ");
                        int age = int.Parse(Console.ReadLine() ?? "0");
                        Console.Write("Введите контакты: ");
                        string cont = Console.ReadLine();
                        bank.AddClient(name, age, cont);
                        break;

                    case "2":
                        Console.Write("Введите ФИО клиента для открытия счета: ");
                        string accName = Console.ReadLine();
                        bank.OpenAccount(accName);
                        break;

                    case "3":
                        Console.Write("Введите ФИО: ");
                        string authName = Console.ReadLine();
                        Console.Write("Введите пароль: ");
                        string pass = Console.ReadLine();
                        bank.AuthenticateClient(authName, pass);
                        break;

                    case "4":
                        Console.Write("Введите сумму для проверки: ");
                        decimal amount = decimal.Parse(Console.ReadLine() ?? "0");
                        bank.CheckTransaction(amount);
                        break;

                    case "5":
                        Console.Write("Текст сообщения: ");
                        string msg = Console.ReadLine();
                        bank.SendNotification(msg);
                        break;

                    case "0":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Неверный ввод.");
                        break;
                }
            }
        }
    }
}