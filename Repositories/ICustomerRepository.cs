using BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ICustomerRepositories
    {
        void SaveCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void SearchCustomer(string searchTerm);
        List<Customer> GetCustomers();
        Customer GetCustomerById(int id);
    }
}