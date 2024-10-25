using System;
using System.Security.Principal;
using Entities.Contracts;
using Exceptions;

namespace Entities
{
    public class Account : IAccount, ICloneable
    {
        private Guid _customerID;
        private Guid _accountID;
        private long _accountNumber;
        private decimal _balance;

        public Guid CustomerID
        {
            get => _customerID;
            set => _customerID = value;
        }

        public Guid AccountID
        {
            get => _accountID;
            set => _accountID = value;
        }

        public long AccountNumber
        {
            get => _accountNumber;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Account number negative.");
                }
                _accountNumber = value;
            }
        }

        public decimal Balance
        {
            get => _balance;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Balance cannot be negative.");
                }
                _balance = value;
            }
        }

        public Account()
        {
            CustomerID = Guid.Empty;
            AccountID = Guid.Empty;
            AccountNumber = 0L;
            Balance = 0.0m;
        }

        public Account(Guid customerID, Guid accountID, long accountNumber, decimal balance)
        {
            CustomerID = customerID;
            AccountID = accountID;
            AccountNumber = accountNumber;
            Balance = balance;
        }

        public object Clone()
        {
            return new Account() { AccountID = this.AccountID, AccountNumber = this.AccountNumber, Balance = this.Balance, CustomerID = this.CustomerID };
        }
    }
}