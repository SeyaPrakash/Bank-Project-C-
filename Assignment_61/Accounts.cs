using System;
using System.Collections.Generic;
using Entities;
using Logic;
using Logic.LogicContracts;
using System.Linq;
using System.Security.Principal;

namespace Assignment_61
{
    static class Accounts
    {
        internal static void AddAccount()
        {
            try
            {
                Account account = new Account();

                ICustomersLogic customersLogic = new CustomersLogic();
                IAccountsLogic accountsLogic = new AccountsLogic();

                if (customersLogic.GetCustomers().Count <= 0)
                {
                    Console.WriteLine("No customers exist");
                    return;
                }
                Console.WriteLine("\n********ADD ACCOUNT*************");
                Customers.ViewCustomers();
                Console.Write("Enter the Customer Code for which you want to create a new account: ");
                long customerCodeToEdit;
                while (!long.TryParse(Console.ReadLine(), out customerCodeToEdit))
                {
                    Console.Write("Enter the Customer Code for which you want to create a new account: ");
                }
                var existingCustomer = customersLogic.GetCustomersByCondition(temp => temp.CustomerCode == customerCodeToEdit).FirstOrDefault();
                if (existingCustomer == null)
                {
                    Console.WriteLine("Invalid Customer Code.\n");
                    return;
                }
                account.CustomerID = existingCustomer.CustomerID;
                Guid newGuid = accountsLogic.AddAccount(account);
                List<Account> matchingAccounts = accountsLogic.GetAccountsByCondition(item => item.AccountID == newGuid);
                if (matchingAccounts.Count >= 1)
                {
                    Console.WriteLine("New Account Number: " + matchingAccounts[0].AccountNumber);
                    Console.WriteLine("Account Added.\n");
                }
                else
                {
                    Console.WriteLine("Account Not added");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }

        internal static void ViewAccounts()
        {
            try
            {
                ICustomersLogic customersLogic = new CustomersLogic();
                IAccountsLogic accountsLogic = new AccountsLogic();

                List<Account> allAccounts = accountsLogic.GetAccounts();

                if (allAccounts.Count == 0)
                {
                    Console.WriteLine("No accounts\n");
                    return;
                }
                Console.WriteLine("\n**********ALL ACCOUNTS*************");
                foreach (var account in allAccounts)
                {
                    Console.WriteLine("Account Number: " + account.AccountNumber);
                    Customer customer = customersLogic.GetCustomersByCondition(temp => temp.CustomerID == account.CustomerID).FirstOrDefault();
                    if (customer != null)
                    {
                        Console.WriteLine("Customer Code: " + customer.CustomerCode);
                        Console.WriteLine("Customer Name: " + customer.CustomerName);
                    }
                    Console.WriteLine("Balance: " + account.Balance);
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }

        internal static void UpdateAccount()
        {
            try
            {
                ICustomersLogic customersLogic = new CustomersLogic();
                IAccountsLogic accountsLogic = new AccountsLogic();

                if (accountsLogic.GetAccounts().Count <= 0)
                {
                    Console.WriteLine("No accounts exist");
                    return;
                }
                Console.WriteLine("\n********EDIT ACCOUNT*************");
                ViewAccounts();
                Console.Write("Enter the Account Number that you want to edit: ");
                long accountCodeToEdit;
                while (!long.TryParse(Console.ReadLine(), out accountCodeToEdit)) { }
                var existingAccount = accountsLogic.GetAccountsByCondition(temp => temp.AccountNumber == accountCodeToEdit).FirstOrDefault();
                if (existingAccount == null)
                {
                    Console.WriteLine("Invalid Account Number.\n");
                    return;
                }
                Console.WriteLine();
                Customers.ViewCustomers();
                Console.Write("Enter the Updated (existing) Customer Code: ");
                long customerCodeToEdit;
                while (!long.TryParse(Console.ReadLine(), out customerCodeToEdit)) { }
                var existingCustomer = customersLogic.GetCustomersByCondition(temp => temp.CustomerCode == customerCodeToEdit).FirstOrDefault();
                if (existingCustomer == null)
                {
                    Console.WriteLine("Invalid Customer Code.\n");
                    return;
                }
                existingAccount.CustomerID = existingCustomer.CustomerID;
                Console.Write("Balance: ");
                existingAccount.Balance = long.Parse(Console.ReadLine());
                bool isUpdated = accountsLogic.UpdateAccount(existingAccount);
                if (isUpdated)
                {
                    Console.WriteLine("Account Updated.\n");
                }
                else
                {
                    Console.WriteLine("Account not updated");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }

        internal static void SearchAccount()
        {
            try
            {
                ICustomersLogic customersLogic = new CustomersLogic();
                IAccountsLogic accountsLogic = new AccountsLogic();

                if (accountsLogic.GetAccounts().Count <= 0)
                {
                    Console.WriteLine("No accounts exist");
                    return;
                }
                Console.WriteLine("\n********SEARCH ACCOUNT*************");
                ViewAccounts();
                Console.Write("Enter the Account Number that you want to get: ");
                long accountCodeToEdit;
                while (!long.TryParse(Console.ReadLine(), out accountCodeToEdit)) { }
                var existingAccount = accountsLogic.GetAccountsByCondition(temp => temp.AccountNumber == accountCodeToEdit).FirstOrDefault();
                if (existingAccount == null)
                {
                    Console.WriteLine("Invalid Account Number.\n");
                    return;
                }
                Console.WriteLine("Account Number: " + existingAccount.AccountNumber);
                Customer customer = customersLogic.GetCustomersByCondition(temp => temp.CustomerID == existingAccount.CustomerID).FirstOrDefault();
                if (customer != null)
                {
                    Console.WriteLine("Customer Code: " + customer.CustomerCode);
                    Console.WriteLine("Customer Name: " + customer.CustomerName);
                }
                Console.WriteLine("Balance: " + existingAccount.Balance);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }

        internal static void DeleteAccount()
        {
            try
            {
                IAccountsLogic accountsLogic = new AccountsLogic();

                if (accountsLogic.GetAccounts().Count <= 0)
                {
                    Console.WriteLine("No accounts exist");
                    return;
                }
                Console.WriteLine("\n********DELETE ACCOUNT*************");
                ViewAccounts();
                Console.Write("Enter the Account Number that you want to delete: ");
                long accountNumberToEdit;
                while (!long.TryParse(Console.ReadLine(), out accountNumberToEdit)) { }
                var existingAccount = accountsLogic.GetAccountsByCondition(temp => temp.AccountNumber == accountNumberToEdit).FirstOrDefault();
                if (existingAccount == null)
                {
                    Console.WriteLine("Invalid Account Number.\n");
                    return;
                }
                bool isDeleted = accountsLogic.DeleteAccount(existingAccount.AccountID);
                if (isDeleted)
                {
                    Console.WriteLine("Account Deleted.\n");
                }
                else
                {
                    Console.WriteLine("Account not deleted");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }
    }
}
