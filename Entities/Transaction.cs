using System;
using Entities.Contracts;
using Exceptions;

namespace Entities
{
    public class Transaction : ITransaction, ICloneable
    {
        private Guid _transactionID;
        private Guid _sourceAccountID;
        private Guid _destinationAccountID;
        private decimal _amount;
        private DateTime _transactionDateTime;

        public Guid TransactionID
        {
            get { return _transactionID; }
            set { _transactionID = value; }
        }

        public Guid SourceAccountID
        {
            get { return _sourceAccountID; }
            set { _sourceAccountID = value; }
        }

        public Guid DestinationAccountID
        {
            get { return _destinationAccountID; }
            set { _destinationAccountID = value; }
        }

        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public DateTime TransactionDateTime
        {
            get { return _transactionDateTime; }
            set { _transactionDateTime = value; }
        }

        public Transaction()
        {
            SourceAccountID = Guid.Empty;
            DestinationAccountID = Guid.Empty;
            Amount = 0.0m;
            TransactionDateTime = DateTime.MinValue;
        }

        public Transaction(Guid sourceAccountID, Guid destinationAccountID, decimal amount, DateTime transactionDateTime)
        {
            SourceAccountID = sourceAccountID;
            DestinationAccountID = destinationAccountID;
            Amount = amount;
            TransactionDateTime = transactionDateTime;
        }

        public object Clone()
        {
            return new Transaction() { SourceAccountID = SourceAccountID, DestinationAccountID = DestinationAccountID, Amount = Amount, TransactionDateTime = TransactionDateTime };
        }
    }
}
