using System;
using System.Collections.Generic;

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
}