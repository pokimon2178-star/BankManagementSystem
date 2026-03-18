using System;
using System.Collections.Generic;
using System.Linq;

namespace BankManagementSystem
{
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
}