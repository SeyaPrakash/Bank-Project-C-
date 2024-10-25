using System;
using System.Collections.Generic;
using Entities;
using Exceptions;
using Data.DataContracts;

namespace Data
{
    public class TransactionData : ITransactionData
    {
        private static List<Transaction> Transactions { set; get; }

        static TransactionData()
        {
            Transactions = new List<Transaction>();
        }

        public List<Transaction> GetTransactions()
        {
            try
            {
                List<Transaction> transactionsList = new List<Transaction>();
                Transactions.ForEach(item => transactionsList.Add(item.Clone() as Transaction));
                return transactionsList;
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
                List<Transaction> transactionsList = new List<Transaction>();
                List<Transaction> filteredTransactions = Transactions.FindAll(predicate);
                filteredTransactions.ForEach(item => transactionsList.Add(item.Clone() as Transaction));
                return transactionsList;
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
                transaction.TransactionID = Guid.NewGuid();
                Transactions.Add(transaction);
                return transaction.TransactionID;
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
                Transaction existingTransaction = Transactions.Find(item => item.TransactionID == transaction.TransactionID);
                if (existingTransaction != null)
                {
                    existingTransaction.SourceAccountID = transaction.SourceAccountID;
                    existingTransaction.DestinationAccountID = transaction.DestinationAccountID;
                    existingTransaction.Amount = transaction.Amount;
                    existingTransaction.TransactionDateTime = transaction.TransactionDateTime;
                    return true;
                }
                else
                {
                    return false;
                }
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
                if (Transactions.RemoveAll(item => item.TransactionID == transactionID) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
