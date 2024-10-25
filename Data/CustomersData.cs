using System;
using System.Collections.Generic;
using Entities;
using Exceptions;
using Data.DataContracts;

namespace Data
{
    public class CustomersData : ICustomersData
    {
        static CustomersData()
        {
            Customers = new List<Customer>()
            {
                new Customer() { CustomerID = Guid.Parse("8C12BEA9-8FB0-4744-8422-1996533805E8"), CustomerCode = 81001, CustomerName = "Seya", Country = "India", City = "Vellore", Address = "xyz", Landmark = "abc", Mobile = "1234567890" }
            };
        }

        private static List<Customer> Customers { set; get; }

        public List<Customer> GetCustomers()
        {
            try
            {
                List<Customer> customersList = new List<Customer>();
                Customers.ForEach(item => customersList.Add(item.Clone() as Customer));
                return customersList;
            }
            catch (CustomerException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Customer> GetCustomersByCondition(Predicate<Customer> predicate)
        {
            try
            {
                List<Customer> customersList = new List<Customer>();
                List<Customer> filteredCustomers = Customers.FindAll(predicate);
                filteredCustomers.ForEach(item => customersList.Add(item.Clone() as Customer));
                return customersList;
            }
            catch (CustomerException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Guid AddCustomer(Customer customer)
        {
            try
            {
                customer.CustomerID = Guid.NewGuid();
                Customers.Add(customer);
                return customer.CustomerID;
            }
            catch (CustomerException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                Customer existingCustomer = Customers.Find(item => item.CustomerID == customer.CustomerID);
                if (existingCustomer != null)
                {
                    existingCustomer.CustomerCode = customer.CustomerCode;
                    existingCustomer.CustomerName = customer.CustomerName;
                    existingCustomer.Address = customer.Address;
                    existingCustomer.Landmark = customer.Landmark;
                    existingCustomer.City = customer.City;
                    existingCustomer.Country = customer.Country;
                    existingCustomer.Mobile = customer.Mobile;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (CustomerException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteCustomer(Guid customerID)
        {
            try
            {
                if (Customers.RemoveAll(item => item.CustomerID == customerID) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (CustomerException)
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