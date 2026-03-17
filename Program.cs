using System;
using System.Collections.Generic;
using System.Linq;

namespace BankManagementSystem
{
    public class Client
    {
        public string FullName { get; set; }
        public Guid Id { get; private set; }
        public string Status { get; set; }
        public List<string> AccountNumbers { get; set; }
        public string Contacts { get; set; }
        public int Age { get; set; }
        public int FailedLoginAttempts { get; set; }

        public Client(string name, int age, string contacts)
        {
            if (age < 18) throw new ArgumentException("Клиент должен быть старше 18 лет.");
            FullName = name;
            Age = age;
            Contacts = contacts;
            Id = Guid.NewGuid();
            Status = "Active";
            AccountNumbers = new List<string>();
            FailedLoginAttempts = 0;
        }
    }

    public class Bank
    {
        private List<Client> _clients = new List<Client>();

        public void AddClient(string name, int age, string contacts)
        {
            try
            {
                var client = new Client(name, age, contacts);
                _clients.Add(client);
                Console.WriteLine($"[Система] Клиент {name} успешно добавлен. ID: {client.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Ошибка] {ex.Message}");
            }
        }

        public bool AuthenticateClient(string name, string password)
        {
            var client = _clients.FirstOrDefault(c => c.FullName.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (client == null)
            {
                Console.WriteLine("[Ошибка] Клиент не найден.");
                return false;
            }

            if (client.Status == "Blocked")
            {
                Console.WriteLine("[Защита] Аккаунт заблокирован.");
                return false;
            }

            if (password == "1234")
            {
                client.FailedLoginAttempts = 0;
                Console.WriteLine($"[Доступ] Добро пожаловать, {client.FullName}!");
                return true;
            }

            client.FailedLoginAttempts++;
            Console.WriteLine($"[Внимание] Неверный пароль. Осталось попыток: {3 - client.FailedLoginAttempts}");

            if (client.FailedLoginAttempts >= 3)
            {
                client.Status = "Blocked";
                Console.WriteLine("[КРИТИЧНО] Превышено число попыток. Аккаунт ЗАБЛОКИРОВАН.");
            }
            return false;
        }

        public void OpenAccount(string name)
        {
            var client = _clients.FirstOrDefault(c => c.FullName.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (client != null)
            {
                string acc = "ACC-" + new Random().Next(100, 999);
                client.AccountNumbers.Add(acc);
                Console.WriteLine($"[Счета] Для {client.FullName} открыт счет: {acc}");
            }
        }

        public void CheckTransaction(decimal amount)
        {
            if (amount > 100000)
                Console.WriteLine("[ALARM] Внимание! Подозрительно большая сумма операции!");
            else
                Console.WriteLine("[Ок] Транзакция обработана.");
        }

        public void SendNotification(string msg)
        {
            int hour = DateTime.Now.Hour;
            if (hour >= 0 && hour < 5)
                Console.WriteLine("[Тихий режим] Ночное время (00-05). Уведомление сохранено в черновики.");
            else
                Console.WriteLine($"[SMS] {msg}");
        }

        public List<Client> GetAllClients() => _clients;
    }

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
                Console.WriteLine("1. Регистрация клиента (add_client)");
                Console.WriteLine("2. Открыть счет (open_account)");
                Console.WriteLine("3. Вход в систему (authenticate_client)");
                Console.WriteLine("4. Проверить сумму операции (security)");
                Console.WriteLine("5. Отправить уведомление (notification)");
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