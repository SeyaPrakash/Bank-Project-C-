using Entities;
using System;
using System.Collections.Generic;

namespace Logic.LogicContracts
{
    public interface IAccountsLogic
    {
        List<Account> GetAccounts();
        List<Account> GetAccountsByCondition(Predicate<Account> predicate);
        Guid AddAccount(Account account);
        bool UpdateAccount(Account account);
        bool DeleteAccount(Guid accountID);
    }
}
