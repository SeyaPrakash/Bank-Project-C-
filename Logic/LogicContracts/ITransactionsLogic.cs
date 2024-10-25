using Entities;
using System;
using System.Collections.Generic;

namespace Logic.LogicContracts
{
    public interface ITransactionsLogic
    {
        List<Transaction> GetTransactions();
        List<Transaction> GetTransactionsByCondition(Predicate<Transaction> predicate);
        Guid AddTransaction(Transaction transaction);
        bool UpdateTransaction(Transaction transaction);
        bool DeleteTransaction(Guid transactionID);
    }
}
