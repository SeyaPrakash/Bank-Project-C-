using System;
using System.Collections.Generic;
using Entities;
using Logic;
using Logic.LogicContracts;
using System.Linq;

namespace Assignment_61
{
    static class Customers
    {
        internal static void AddCustomer()
        {
            try
            {
                Customer customer = new Customer();

                Console.WriteLine("\n********ADD CUSTOMER*************");
                Console.Write("Customer Name: ");
                customer.CustomerName = Console.ReadLine();
                Console.Write("Address: ");
                customer.Address = Console.ReadLine();
                Console.Write("Landmark: ");
                customer.Landmark = Console.ReadLine();
                Console.Write("City: ");
                customer.City = Console.ReadLine();
                Console.Write("Country: ");
                customer.Country = Console.ReadLine();
                Console.Write("Mobile: ");
                customer.Mobile = Console.ReadLine();

                ICustomersLogic customersLogic = new CustomersLogic();
                Guid newGuid = customersLogic.AddCustomer(customer);

                List<Customer> matchingCustomers = customersLogic.GetCustomersByCondition(item => item.CustomerID == newGuid);
                if (matchingCustomers.Count >= 1)
                {
                    Console.WriteLine("New Customer Code: " + matchingCustomers[0].CustomerCode);
                    Console.WriteLine("Customer Added.\n");
                }
                else
                {
                    Console.WriteLine("Customer Not added");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }

        internal static void ViewCustomers()
        {
            try
            {
                ICustomersLogic customersLogic = new CustomersLogic();

                List<Customer> allCustomers = customersLogic.GetCustomers();
                if (allCustomers.Count == 0)
                {
                    Console.WriteLine("No customers\n");
                    return;
                }
                Console.WriteLine("\n**********ALL CUSTOMERS*************");
                foreach (var customer in allCustomers)
                {
                    Console.WriteLine("Customer Code: " + customer.CustomerCode);
                    Console.WriteLine("Customer Name: " + customer.CustomerName);
                    Console.WriteLine("Address: " + customer.Address);
                    Console.WriteLine("Landmark: " + customer.Landmark);
                    Console.WriteLine("City: " + customer.City);
                    Console.WriteLine("Country: " + customer.Country);
                    Console.WriteLine("Mobile: " + customer.Mobile);
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }

        internal static void UpdateCustomer()
        {
            try
            {
                ICustomersLogic customersLogic = new CustomersLogic();

                if (customersLogic.GetCustomers().Count <= 0)
                {
                    Console.WriteLine("No customers exist");
                    return;
                }
                Console.WriteLine("\n********EDIT CUSTOMER*************");
                ViewCustomers();
                Console.Write("Enter the Customer Code that you want to edit: ");
                long customerCodeToEdit;
                while (!long.TryParse(Console.ReadLine(), out customerCodeToEdit)) { }
                var existingCustomer = customersLogic.GetCustomersByCondition(temp => temp.CustomerCode == customerCodeToEdit).FirstOrDefault();
                if (existingCustomer == null)
                {
                    Console.WriteLine("Invalid Customer Code.\n");
                    return;
                }
                Console.WriteLine("NEW CUSTOMER DETAILS:");
                Console.Write("Customer Name: ");
                existingCustomer.CustomerName = Console.ReadLine();
                Console.Write("Address: ");
                existingCustomer.Address = Console.ReadLine();
                Console.Write("Landmark: ");
                existingCustomer.Landmark = Console.ReadLine();
                Console.Write("City: ");
                existingCustomer.City = Console.ReadLine();
                Console.Write("Country: ");
                existingCustomer.Country = Console.ReadLine();
                Console.Write("Mobile: ");
                existingCustomer.Mobile = Console.ReadLine();
                bool isUpdated = customersLogic.UpdateCustomer(existingCustomer);
                if (isUpdated)
                {
                    Console.WriteLine("Customer Updated.\n");
                }
                else
                {
                    Console.WriteLine("Customer not updated");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }

        internal static void SearchCustomer()
        {
            try
            {
                ICustomersLogic customersLogic = new CustomersLogic();

                if (customersLogic.GetCustomers().Count <= 0)
                {
                    Console.WriteLine("No customers exist");
                    return;
                }
                Console.WriteLine("\n********SEARCH CUSTOMER*************");
                ViewCustomers();
                Console.Write("Enter the Customer Code that you want to get: ");
                long customerCodeToEdit;
                while (!long.TryParse(Console.ReadLine(), out customerCodeToEdit)) { }
                var existingCustomer = customersLogic.GetCustomersByCondition(temp => temp.CustomerCode == customerCodeToEdit).FirstOrDefault();
                if (existingCustomer == null)
                {
                    Console.WriteLine("Invalid Customer Code.\n");
                    return;
                }
                Console.WriteLine("Customer Code: " + existingCustomer.CustomerCode);
                Console.WriteLine("Customer Name: " + existingCustomer.CustomerName);
                Console.WriteLine("Address: " + existingCustomer.Address);
                Console.WriteLine("Landmark: " + existingCustomer.Landmark);
                Console.WriteLine("City: " + existingCustomer.City);
                Console.WriteLine("Country: " + existingCustomer.Country);
                Console.WriteLine("Mobile: " + existingCustomer.Mobile);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }

        internal static void DeleteCustomer()
        {
            try
            {
                ICustomersLogic customersLogic = new CustomersLogic();

                if (customersLogic.GetCustomers().Count <= 0)
                {
                    Console.WriteLine("No customers exist");
                    return;
                }
                Console.WriteLine("\n********DELETE CUSTOMER*************");
                ViewCustomers();
                Console.Write("Enter the Customer Code that you want to delete: ");
                long customerCodeToEdit;
                while (!long.TryParse(Console.ReadLine(), out customerCodeToEdit)) { }
                var existingCustomer = customersLogic.GetCustomersByCondition(temp => temp.CustomerCode == customerCodeToEdit).FirstOrDefault();
                if (existingCustomer == null)
                {
                    Console.WriteLine("Invalid Customer Code.\n");
                    return;
                }
                bool isDeleted = customersLogic.DeleteCustomer(existingCustomer.CustomerID);
                if (isDeleted)
                {
                    Console.WriteLine("Customer Deleted.\n");
                }
                else
                {
                    Console.WriteLine("Customer not deleted");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.GetType());
            }
        }
    }
}
