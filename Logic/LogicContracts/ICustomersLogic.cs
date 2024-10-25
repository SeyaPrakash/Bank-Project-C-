using Entities;
using System;
using System.Collections.Generic;

namespace Logic.LogicContracts
{
    public interface ICustomersLogic
    {
        List<Customer> GetCustomers();
        List<Customer> GetCustomersByCondition(Predicate<Customer> predicate);
        Guid AddCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool DeleteCustomer(Guid customerID);
    }
}
