using BusinessObjects.Model;
using DataAccessObjects;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepositories
    {
        public void DeleteCustomer(Customer customer)
        => CustomerDAO.getInstance().DeleteCustomer(customer);

        public Customer GetCustomerById(int id)
        => CustomerDAO.getInstance().GetCustomerById(id);

        public List<Customer> GetCustomers()
        => CustomerDAO.getInstance().GetCustomers();

        public void SaveCustomer(Customer customer)
        => CustomerDAO.getInstance().SaveCustomer(customer);

        public void UpdateCustomer(Customer customer)
        => CustomerDAO.getInstance().UpdateCustomer(customer);

        public void SearchCustomer(string searchTerm)
        => CustomerDAO.getInstance().SearchCustomer(searchTerm);

    }
}