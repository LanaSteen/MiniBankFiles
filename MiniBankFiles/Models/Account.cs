using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MiniBankFiles.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string iban;
        public string currency;
        public string Iban
        {
            get => iban;
            set
            {
                if (value.Length != 22)
                    throw new ArgumentException("IBAN must be 22 characters long.");
                iban = value;
            }
        }

        public string Currency
        {
            get => currency;
            set
            {
                if (value.Length != 3)
                    throw new ArgumentException("Currency code must be 3 characters long.");
                currency = value.ToUpper();
            }
        }
        public decimal Balance { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }



        public static Account Parse(string line)
        {
            string[] values = line.Split(',');
            return new Account
            {
                Id = int.Parse(values[0]),
                Iban = values[1],
                Currency = values[2],
                Balance = decimal.Parse(values[3]),
                CustomerId = int.Parse(values[4]),
                Name = values[5]
            };
        }

        public static Account[] ParseAll(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var accounts = new Account[lines.Length - 1];

            for (int i = 1; i < lines.Length; i++)
            {
                accounts[i - 1] = Parse(lines[i]);
            }

            return accounts;
        }

        public override string ToString()
        {
            return $"(ID: {Id}, IBAN: {Iban}, Balance: {Balance} {Currency} - {Name})";
        }



        public static void AddAccount(string filePath, Account newAccount, string customerFilePath)
        {
            var accounts = ParseAll(filePath);
            var customers = Customer.ParseAll(customerFilePath);

            bool customerExists = false;
            foreach (var customer in customers)
            {
                if (customer.Id == newAccount.CustomerId)
                {
                    customerExists = true;
                    break;
                }
            }

            if (!customerExists)
            {
                throw new ArgumentException("The specified Customer ID does not exist.");
            }

            int nextId = (accounts.Length > 0) ? accounts[^1].Id + 1 : 1;

            foreach (var account in accounts)
            {
                if (account.Iban.Equals(newAccount.Iban, StringComparison.OrdinalIgnoreCase))
                {
                    throw new ArgumentException("IBAN must be unique.");
                }
            }

            newAccount.Id = nextId;
            string line = $"{newAccount.Id},{newAccount.Iban},{newAccount.Currency},{newAccount.Balance},{newAccount.CustomerId},{newAccount.Name}";
            File.AppendAllText(filePath, line + Environment.NewLine);
        }
    }
}
