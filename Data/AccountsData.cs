using System;
using System.Collections.Generic;
using Entities;
using Exceptions;
using Data.DataContracts;
using System.Security.Principal;

namespace Data
{
    public class AccountsData : IAccountsData
    {
        static AccountsData()
        {
            Accounts = new List<Account>()
            {
                new Account() { AccountID = Guid.Parse("E3B7F3CB-1315-431B-8E60-4FE6D79084C8"), AccountNumber = 1000000001, Balance = 10000, CustomerID = Guid.Parse("8C12BEA9-8FB0-4744-8422-1996533805E8") },
                new Account() { AccountID = Guid.Parse("68319657-1FCF-49CC-9193-C4442F55AD28"), AccountNumber = 1000000002, Balance = 500, CustomerID = Guid.Parse("8C12BEA9-8FB0-4744-8422-1996533805E8") },
            };
        }
        private static List<Account> Accounts { set; get; }

        public List<Account> GetAccounts()
        {
            try
            {
                List<Account> accountsList = new List<Account>();
                Accounts.ForEach(item => accountsList.Add(item.Clone() as Account));
                return accountsList;
            }
            catch (AccountException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Account> GetAccountsByCondition(Predicate<Account> predicate)
        {
            try
            {
                List<Account> accountsList = new List<Account>();
                List<Account> filteredAccounts = Accounts.FindAll(predicate);
                filteredAccounts.ForEach(item => accountsList.Add(item.Clone() as Account));
                return accountsList;
            }
            catch (AccountException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Guid AddAccount(Account account)
        {
            try
            {
                account.AccountID = Guid.NewGuid();
                Accounts.Add(account);
                return account.AccountID;
            }
            catch (AccountException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateAccount(Account account)
        {
            try
            {
                Account existingAccount = Accounts.Find(item => item.AccountID == account.AccountID);
                if (existingAccount != null)
                {
                    existingAccount.AccountID = account.AccountID;
                    existingAccount.AccountNumber = account.AccountNumber;
                    existingAccount.CustomerID = account.CustomerID;
                    existingAccount.Balance = account.Balance;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (AccountException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteAccount(Guid accountID)
        {
            try
            {
                if (Accounts.RemoveAll(item => item.AccountID == accountID) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (AccountException)
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
