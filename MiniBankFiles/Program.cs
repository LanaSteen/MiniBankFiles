using MiniBankFiles.Models;

namespace MiniBankFiles
{
    internal class Program
    {
        static void Main(string[] args)
        {

            try
            {
                string filePathAcc = @"../../../Files/Accounts.csv";
                string filePathCus = @"../../../Files/Customers.csv";

                Account[] accounts = Account.ParseAll(filePathAcc);
                Customer[] customers = Customer.ParseAll(filePathCus);


                Console.WriteLine("Creating Customer");
                Console.WriteLine("Write Name");
                string name = Console.ReadLine();
                Console.WriteLine("Write IdentityNumber");
                string identityNumber = Console.ReadLine();
                Console.WriteLine("Write PhoneNumber");
                string phoneNumber = Console.ReadLine();
                Console.WriteLine("Write Email");
                string email = Console.ReadLine();
                Console.WriteLine("Write Type(0 or 1) ");
                string _type = Console.ReadLine();

                CustomerType.TryParse(_type, out CustomerType type);

                Customer newCustomer = new Customer
                {
                    Name = name,
                    IdentityNumber = identityNumber,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    Type = type
                };


                int newCustomerId = Customer.AddCustomer(filePathCus, newCustomer);
                Console.WriteLine($"New customer added successfully on ID: {newCustomerId}.");

                Console.WriteLine("Creating Account");
                Console.WriteLine("Write Iban 22 length");
                string iban = Console.ReadLine();
                Console.WriteLine("Write Currency 3 letter length");
                string currency = Console.ReadLine();
                Console.WriteLine("Write Balance");
                string _balance = Console.ReadLine();
                Console.WriteLine("Write CustomerId");
                string _customerId = Console.ReadLine();
                Console.WriteLine("Write Account Name ");
                string accName = Console.ReadLine();

                int.TryParse(_balance, out int balance);
                int.TryParse(_customerId, out int customerId);

                Account newAccount = new Account
                {
                    Iban = iban,
                    Currency = currency,
                    Balance = balance,
                    CustomerId = customerId,
                    Name = accName
                };

                Account.AddAccount(filePathAcc, newAccount, filePathCus);
                Console.WriteLine("New account added successfully.");




                Console.WriteLine("Write Id Of Customer You Want To Get Info About");
                string custId = Console.ReadLine();
                int.TryParse(custId, out int searchCustomerId);
                if (searchCustomerId > 0) {
                    Customer.DisplayCustomerWithAccounts(filePathCus, filePathAcc, searchCustomerId);
                }
                else
                {
                    Console.WriteLine("There is no Customer with such an ID");
                }

               
                Console.WriteLine("Write YES if you want to display all Customers and Accounts if not then any key");
                string userInp = Console.ReadLine().ToUpper().Trim();
                if (userInp == "YES")
                {
                    Account[] accountsLastModified = Account.ParseAll(filePathAcc);
                    Customer[] customerstsLastModified = Customer.ParseAll(filePathCus);
                    Console.WriteLine("All Customers");

                    foreach (Customer customer in customerstsLastModified)
                    {
                        Console.WriteLine(customer);
                    }
                    Console.WriteLine("All Accounts");

                    foreach (Account account in accountsLastModified)
                    {
                        Console.WriteLine(account);

                    }
                }
            



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
