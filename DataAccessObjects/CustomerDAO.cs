using BusinessObjects.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class CustomerDAO
    {
        private static CustomerDAO customerDAO;

        public static CustomerDAO getInstance()
        {
            if (customerDAO == null)
            {
                customerDAO = new CustomerDAO();
            }
            return customerDAO;
        }

        public List<Customer> GetCustomers()
        {
            var listCustomer = new List<Customer>();
            try
            {
                using var context = new FmartDbContext();
                listCustomer = context.Customers.Include(c => c.Orders).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listCustomer;
        }

        public void SearchCustomer(string searchTerm)
        {
            try
            {
                using var context = new FmartDbContext();
                List<Customer> listCustomer = context.Customers
                                      .Where(b => b.FullName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

                if (listCustomer.Any())
                {
                    foreach (Customer Customer in listCustomer)
                    {
                        Console.WriteLine(Customer);
                    }
                }
                else
                {
                    Console.WriteLine("No Customer found!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while searching for Customers: " + ex.Message);
            }
        }

        public void SaveCustomer(Customer c)
        {
            try
            {
                using var context = new FmartDbContext();
                context.Customers.Add(c);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            try
            {
                using var context = new FmartDbContext();
                context.Entry<Customer>(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteCustomer(Customer customer)
        {
            try
            {
                using var context = new FmartDbContext();
                var c = context.Customers.SingleOrDefault(c => c.CustomerId == customer.CustomerId);
                if (c != null)
                {
                    context.Customers.Remove(c);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Customer? GetCustomerById(int id)
        {
            Customer customer;
            try
            {
                using var context = new FmartDbContext();
                customer = context.Customers.Include(c => c.Orders).FirstOrDefault(r => r.CustomerId.Equals(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customer;
        }

        //public Customer? GetCustomerFullName(string name)
        //{
        //    Customer customer;
        //    try
        //    {
        //        using var context = new FmartDbContext();
        //        customer = context.Customers.FirstOrDefault(r => r.GetFullName().Equals(name));
        //    }
        //    catch(Exception ex) {
        //        throw new Exception(ex.Message);
        //    }

        //    return customer;
        //}
    }
}