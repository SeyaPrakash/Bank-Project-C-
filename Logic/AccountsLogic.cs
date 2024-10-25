using System;
using System.Collections.Generic;
using Logic.LogicContracts;
using Data.DataContracts;
using Data;
using Entities;
using Exceptions;

namespace Logic
{
    public class AccountsLogic : IAccountsLogic
    {
        private IAccountsData AccountsData { get; set; }

        public AccountsLogic()
        {
            AccountsData = new AccountsData();
        }

        public List<Account> GetAccounts()
        {
            try
            {
                return AccountsData.GetAccounts();
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
                return AccountsData.GetAccountsByCondition(predicate);
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
                List<Account> allAccounts = AccountsData.GetAccounts();
                long maxAccountNumber = 0;
                foreach (var item in allAccounts)
                {
                    if (item.AccountNumber > maxAccountNumber)
                    {
                        maxAccountNumber = item.AccountNumber;
                    }
                }
                if (allAccounts.Count >= 1)
                {
                    account.AccountNumber = maxAccountNumber + 1;
                }
                else
                {
                    account.AccountNumber = Configuration.Settings.BaseAccountNo + 1;
                }
                account.Balance = 0.0M;
                return AccountsData.AddAccount(account);
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
                return AccountsData.UpdateAccount(account);
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
                return AccountsData.DeleteAccount(accountID);
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
