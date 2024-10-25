using System;
using System.Collections.Generic;
using System.Linq;
using Logic.LogicContracts;
using Data.DataContracts;
using Data;
using Entities;
using Exceptions;

namespace Logic
{
    public class TransactionsLogic : ITransactionsLogic
    {
        private ITransactionData TransactionsData { get; set; }
        private IAccountsData AccountsData { get; set; }


        public TransactionsLogic()
        {
            TransactionsData = new TransactionData();
            AccountsData = new AccountsData();
        }

        public List<Transaction> GetTransactions()
        {
            try
            {
                return TransactionsData.GetTransactions();
            }
            catch (TransactionException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Transaction> GetTransactionsByCondition(Predicate<Transaction> predicate)
        {
            try
            {
                return TransactionsData.GetTransactionsByCondition(predicate);
            }
            catch (TransactionException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Guid AddTransaction(Transaction transaction)
        {
            try
            {
                var sourceAccount = AccountsData.GetAccountsByCondition(temp => temp.AccountID == transaction.SourceAccountID).FirstOrDefault();
                var destinationAccount = AccountsData.GetAccountsByCondition(temp => temp.AccountID == transaction.DestinationAccountID).FirstOrDefault();
                if (sourceAccount != null && destinationAccount != null)
                {
                    if (sourceAccount.Balance < transaction.Amount)
                    {
                        throw new TransactionException($"Source account has insuffient funds for transaction of {transaction.Amount}");
                    }
                    sourceAccount.Balance -= transaction.Amount;
                    destinationAccount.Balance += transaction.Amount;
                    var newTransactionID = TransactionsData.AddTransaction(transaction);
                    AccountsData.UpdateAccount(sourceAccount);
                    AccountsData.UpdateAccount(destinationAccount);
                    return newTransactionID;
                }
                throw new TransactionException("Source account or destination account number is invalid");
            }
            catch (TransactionException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateTransaction(Transaction transaction)
        {
            try
            {
                return TransactionsData.UpdateTransaction(transaction);
            }
            catch (TransactionException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteTransaction(Guid transactionID)
        {
            try
            {
                return TransactionsData.DeleteTransaction(transactionID);
            }
            catch (TransactionException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
