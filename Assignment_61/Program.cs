using System;

namespace Assignment_61
{
    public static class Program
    {
        static void Main()
        {
            Console.WriteLine("************** Seya Bank *****************");
            Console.WriteLine("::Login Page::");
            string userName = null, password = null;

            while (true)
            {
                Console.Write("Username (Press ENTER to exit): ");
                userName = Console.ReadLine();

                if (userName != "")
                {
                    Console.Write("Password: ");
                    password = Console.ReadLine();
                }
                else
                {
                    break;
                }
                int mainMenuChoice = -1;

                if (userName == "admin" && password == "admin")
                {
                    do
                    {
                        Console.WriteLine("\n:::Main menu:::");
                        Console.WriteLine("1. Customers");
                        Console.WriteLine("2. Accounts");
                        Console.WriteLine("3. Funds Transfer");
                        Console.WriteLine("4. Account Statement");
                        Console.WriteLine("0. Exit");

                        Console.Write("Enter choice: ");
                        while (!int.TryParse(Console.ReadLine(), out mainMenuChoice))
                        {
                            Console.Write("Enter choice: ");
                        }
                        switch (mainMenuChoice)
                        {
                            case 1: CustomersMenu(); break;
                            case 2: AccountsMenu(); break;
                            case 3: FundTransfer.AddTransaction(); break;
                            case 4: FundTransfer.ViewTransactions(); break;
                            case 0: break;
                        }
                    } while (mainMenuChoice != 0);
                }
                else
                {
                    Console.WriteLine("Invalid username or password.\n");
                }

                if (mainMenuChoice == 0)
                    break;
            }
            Console.WriteLine("Thank you! Visit again.");
            Console.ReadKey();
        }

        static void CustomersMenu()
        {
            int customerMenuChoice = -1;
            do
            {
                Console.WriteLine("\n:::Customers menu:::");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. Delete Customer");
                Console.WriteLine("3. Update Customer");
                Console.WriteLine("4. Search Customers");
                Console.WriteLine("5. View Customers");
                Console.WriteLine("0. Back to Main Menu");
                Console.Write("Enter choice: ");
                customerMenuChoice = Convert.ToInt32(Console.ReadLine());
                switch (customerMenuChoice)
                {
                    case 1: Customers.AddCustomer(); break;
                    case 2: Customers.DeleteCustomer(); break;
                    case 3: Customers.UpdateCustomer(); break;
                    case 4: Customers.SearchCustomer(); break;
                    case 5: Customers.ViewCustomers(); break;
                }
            } while (customerMenuChoice != 0);
        }

        static void AccountsMenu()
        {
            int accountsMenuChoice;
            do
            {
                Console.WriteLine("\n:::Accounts menu:::");
                Console.WriteLine("1. Add Account");
                Console.WriteLine("2. Delete Account");
                Console.WriteLine("3. Update Account");
                Console.WriteLine("4. Search Accounts");
                Console.WriteLine("5. View Accounts");
                Console.WriteLine("0. Back to Main Menu");
                Console.Write("Enter choice: ");
                while (!(int.TryParse(Console.ReadLine(), out accountsMenuChoice)))
                {
                    Console.Write("Enter choice: ");
                }
                switch (accountsMenuChoice)
                {
                    case 1: Accounts.AddAccount(); break;
                    case 2: Accounts.DeleteAccount(); break;
                    case 3: Accounts.UpdateAccount(); break;
                    case 4: Accounts.SearchAccount(); break;
                    case 5: Accounts.ViewAccounts(); break;
                }
            } while (accountsMenuChoice != 0);
        }
    }
}
