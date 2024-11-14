using BusinessObjects.Model;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartSupermarketFMartWPF
{
    /// <summary>
    /// Interaction logic for CustomerManagementPage.xaml
    /// </summary>
    public partial class CustomerManagementPage : Page
    {

        private CustomerRepository customerRepository = new CustomerRepository();

        public CustomerManagementPage()
        {
            InitializeComponent();
            LoadCustomers();
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomerListView.SelectedItem is Customer selectedCustomer)
            {
                txtCustomerId.Text = selectedCustomer.CustomerId.ToString();
                //txtFullName.Text = selectedCustomer.FullName;
                txtFirstName.Text = selectedCustomer.FirstName;
                txtLastName.Text = selectedCustomer.LastName;
                txtEmail.Text = selectedCustomer.Email;
                txtPhone.Text = selectedCustomer.Phone;
                txtAddress.Text = selectedCustomer.Address;

            }
        }

        private void LoadCustomers()
        {
            try
            {
                CustomerListView.SelectedItem = null;
                CustomerListView.ItemsSource = customerRepository.GetCustomers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            var name = txtFullName.Text;
            var customers = customerRepository.GetCustomers();

            if (!string.IsNullOrEmpty(name))
                customers = customers.Where(c => c.FullName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            CustomerListView.ItemsSource = customers;
        }

        private void CreateClick(object sender, RoutedEventArgs e)
        {
            var newCustomer = new Customer
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Email = txtEmail.Text,
                Phone = txtPhone.Text,
                Address = txtAddress.Text
            };

            customerRepository.SaveCustomer(newCustomer);
            LoadCustomers();
        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            if (CustomerListView.SelectedItem is Customer selectedCustomer)
            {
                selectedCustomer.FirstName = txtFirstName.Text;
                selectedCustomer.LastName = txtLastName.Text;
                selectedCustomer.Email = txtEmail.Text;
                selectedCustomer.Phone = txtPhone.Text;
                selectedCustomer.Address = txtAddress.Text;
                customerRepository.UpdateCustomer(selectedCustomer);
                LoadCustomers();
            }
            else
            {
                MessageBox.Show("Please select a customer to edit.");
            }
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if (CustomerListView.SelectedItem is Customer selectedCustomer)
            {
                var result = MessageBox.Show("Are you sure you want to delete this custpmer?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    customerRepository.DeleteCustomer(selectedCustomer);
                    LoadCustomers();
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to delete.");
            }
        }
    }
}
