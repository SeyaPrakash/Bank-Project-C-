using Entities;
using System;
using System.Collections.Generic;

namespace Data.DataContracts
{
    public interface ICustomersData
    {
        List<Customer> GetCustomers();
        List<Customer> GetCustomersByCondition(Predicate<Customer> predicate);
        Guid AddCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool DeleteCustomer(Guid customerID);
    }
}
