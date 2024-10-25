using System;
using Entities;
using Logic;
using Logic.LogicContracts;
using System.Linq;
using System.Security.Principal;

namespace Assignment_61
{
    static class FundTransfer
    {
        internal static void AddTransaction()
        {
            try
            {
                Transaction transaction = new Transaction();

                ICustomersLogic customersLogic = new CustomersLogic();
                IAccountsLogic accountsLogic = new AccountsLogic();
                ITransactionsLogic transactionsLogic = new TransactionsLogic();

                Accounts.ViewAccounts();

                Console.Write("Enter the Source Account Number: ");
                long sourceAccountNumber;
                Account sourceAccount;
                while (!long.TryParse(Console.ReadLine(), out sourceAccountNumber))
                {
                    Console.Write("Enter the Source Account Number: ");
                }
                sourceAccount = accountsLogic.GetAccountsByCondition(temp => temp.AccountNumber == sourceAccountNumber).FirstOrDefault();
                if (sourceAccount == null)
                {
                    Console.WriteLine("Invalid Account Number.\n");
                    return;
                }
                Console.Write("Enter the Destination Account Number: ");
                long destinationAccountNumber = -1;
                Account destinationAccount;
                bool isDestinationAccountNumberValid = false;
                while (!isDestinationAccountNumberValid)
                {
                    while (!long.TryParse(Console.ReadLine(), out destinationAccountNumber))
                    {
                        Console.Write("Enter the Destination Account Number: ");
                    }

                    if (destinationAccountNumber != sourceAccountNumber)
                    {
                        isDestinationAccountNumberValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Source account number and destination account number can't be same");
                    }
                }
                destinationAccount = accountsLogic.GetAccountsByCondition(temp => temp.AccountNumber == destinationAccountNumber).FirstOrDefault();
                if (destinationAccount == null)
                {
                    Console.WriteLine("Invalid Account Number.\n");
                    return;
                }
                Console.WriteLine("Date of Transaction (YYYY-MM-DD hh:mm:ss tt): ");
                DateTime dateOfTransaction;
                while (!DateTime.TryParse(Console.ReadLine(), out dateOfTransaction))
                {
                    Console.WriteLine("Date of Transaction (YYYY-MM-DD hh:mm:ss tt): ");
                }
                transaction.TransactionDateTime = dateOfTransaction;
                Console.Write("Amount: ");
                decimal amount;
                while (!decimal.TryParse(Console.ReadLine(), out amount))
                {
                    Console.Write("Amount: ");
                }
                transaction.Amount = amount;
                transaction.SourceAccountID = sourceAccount.AccountID;
                transaction.DestinationAccountID = destinationAccount.AccountID;
                Guid newGuid = transactionsLogic.AddTransaction(transaction);
                Console.WriteLine("Transaction successful");
                var updatedSourceAccount = accountsLogic.GetAccountsByCondition(temp => temp.AccountNumber == sourceAccount.AccountNumber).FirstOrDefault();
                if (updatedSourceAccount != null)
                {
                    Console.WriteLine($"Account Balance of source account number {updatedSourceAccount.AccountNumber} is: {updatedSourceAccount.Balance}.");
                }
                var updatedDestinationAccount = accountsLogic.GetAccountsByCondition(temp => temp.AccountNumber == destinationAccount.AccountNumber).FirstOrDefault();
                if (updatedDestinationAccount != null)
                {
                    Console.WriteLine($"Account Balance of destination account number {updatedDestinationAccount.AccountNumber} is: {updatedDestinationAccount.Balance}.\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }

        internal static void ViewTransactions()
        {
            try
            {
                ICustomersLogic customersLogic = new CustomersLogic();
                IAccountsLogic accountsLogic = new AccountsLogic();
                ITransactionsLogic transactionsLogic = new TransactionsLogic();

                if (accountsLogic.GetAccounts().Count <= 0)
                {
                    Console.WriteLine("No accounts exist");
                    return;
                }
                Console.WriteLine("\n********ACCOUNT STATEMENT*************");
                Accounts.ViewAccounts();
                Console.Write("Enter the Account Number that you want to view: ");
                long accountNumberToSearch;
                while (!long.TryParse(Console.ReadLine(), out accountNumberToSearch)) { }
                var existingAccount = accountsLogic.GetAccountsByCondition(temp => temp.AccountNumber == accountNumberToSearch).FirstOrDefault();
                if (existingAccount == null)
                {
                    Console.WriteLine("Invalid Account Number.\n");
                    return;
                }
                Console.WriteLine();
                var debitTransactions = transactionsLogic.GetTransactionsByCondition(temp => temp.SourceAccountID == existingAccount.AccountID).OrderBy(temp => temp.TransactionDateTime).ToList();
                var creditTransactions = transactionsLogic.GetTransactionsByCondition(temp => temp.DestinationAccountID == existingAccount.AccountID).OrderBy(temp => temp.TransactionDateTime).ToList();
                Console.WriteLine("Debit Transactions:");
                if (debitTransactions.Count > 0)
                {
                    Console.WriteLine($"Transaction Date, Source Account Number, Destination Account Number, Transaction Amount");
                    foreach (var transaction in debitTransactions)
                    {
                        var sourceAccount = accountsLogic.GetAccountsByCondition(temp => temp.AccountID == transaction.SourceAccountID).FirstOrDefault();
                        var destinationAccount = accountsLogic.GetAccountsByCondition(temp => temp.AccountID == transaction.DestinationAccountID).FirstOrDefault();

                        if (sourceAccount != null && destinationAccount != null)
                        {
                            Console.WriteLine($"{transaction.TransactionDateTime}, {sourceAccount.AccountNumber}, {destinationAccount.AccountNumber}, {transaction.Amount}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No debit transactions");
                }
                Console.WriteLine("\nCredit Transactions:");
                if (creditTransactions.Count > 0)
                {
                    Console.WriteLine($"Transaction Date, Source Account Number, Destination Account Number, Transaction Amount");
                    foreach (var transaction in creditTransactions)
                    {
                        var sourceAccount = accountsLogic.GetAccountsByCondition(temp => temp.AccountID == transaction.SourceAccountID).FirstOrDefault();
                        var destinationAccount = accountsLogic.GetAccountsByCondition(temp => temp.AccountID == transaction.DestinationAccountID).FirstOrDefault();

                        if (sourceAccount != null && destinationAccount != null)
                        {
                            Console.WriteLine($"{transaction.TransactionDateTime}, {sourceAccount.AccountNumber}, {destinationAccount.AccountNumber}, {transaction.Amount}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No credit transactions");
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
