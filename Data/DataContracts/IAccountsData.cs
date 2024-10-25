using Entities;
using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Data.DataContracts
{
    public interface IAccountsData
    {
        List<Account> GetAccounts();
        List<Account> GetAccountsByCondition(Predicate<Account> predicate);
        Guid AddAccount(Account account);
        bool UpdateAccount(Account account);
        bool DeleteAccount(Guid accountID);
    }
}
