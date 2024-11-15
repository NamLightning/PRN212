﻿using BusinessObjects.Model;
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
using static MaterialDesignThemes.Wpf.Theme;

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
                txtFirstName.Text = selectedCustomer.FirstName;
                txtLastName.Text = selectedCustomer.LastName;
                txtPhone.Text = selectedCustomer.Phone;

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

        private void ClearInput()
        {
            txtCustomerId.Text = "";
            txtFullName.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPhone.Text = "";
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
                Phone = txtPhone.Text
            };

            customerRepository.SaveCustomer(newCustomer);
            LoadCustomers();
            ClearInput();
        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            if (CustomerListView.SelectedItem is Customer selectedCustomer)
            {
                selectedCustomer.FirstName = txtFirstName.Text;
                selectedCustomer.LastName = txtLastName.Text;
                selectedCustomer.Phone = txtPhone.Text;
                customerRepository.UpdateCustomer(selectedCustomer);
                LoadCustomers();
                ClearInput();
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
                    ClearInput();
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to delete.");
            }
        }
    }
}
