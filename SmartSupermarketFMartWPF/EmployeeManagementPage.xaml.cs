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
    /// Interaction logic for EmployeeManagementPage.xaml
    /// </summary>
    public partial class EmployeeManagementPage : Page
    {
        private IEmployeeRepositoty empRepo = new EmployeeRepositoty();
        public EmployeeManagementPage()
        {
            InitializeComponent();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            try 
            { 
            dgData.ItemsSource = null;
            dgData.ItemsSource = empRepo.GetEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}
        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedIndex == -1) return;

            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid == null) return;

            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            if (row == null) return;

            DataGridCell rowColumn = dataGrid.Columns[0].GetCellContent(row)?.Parent as DataGridCell;
            if (rowColumn == null) return;

            string id = (rowColumn.Content as TextBlock)?.Text;
            if (string.IsNullOrEmpty(id)) return;

            if (int.TryParse(id, out int EmployeeId))
            {
                Employee emp = empRepo.GetEmployeeById(EmployeeId);
                if (emp != null)
                {
                    txtFirstName.Text = emp.FirstName;
                    txtLastName.Text = emp.LastName;
                    txtPosition.Text = emp.Position;
                    txtPhone.Text = emp.Phone;
                    txtEmail.Text = emp.Email;
                }
            }
        }
        private void resetInput()
        {
            txtEmployeeID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPosition.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
        }
        private void btnAdd_click(object sender, RoutedEventArgs e)
        {
            if (txtFirstName == null || txtLastName == null || txtPosition == null || txtPhone == null || txtEmail == null)
            {
                MessageBox.Show("One or more UI elements are not initialized.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter the first name.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please enter the last name.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPosition.Text))
            {
                MessageBox.Show("Please enter the position.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Please enter a phone number.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }
            try
            {
                Employee employee = new Employee();
                employee.FirstName = txtFirstName.Text;
                employee.LastName = txtLastName.Text;
                employee.Position = txtPosition.Text;
                employee.Phone = txtPhone.Text;
                employee.Email = txtEmail.Text;
                empRepo.SaveEmployee(employee);

            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadEmployees();
            }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private void btnEdit_click(object sender, RoutedEventArgs e)
        {
            if (dgData.SelectedItem is Employee selectedEmployee)
            {
                selectedEmployee.FirstName = txtFirstName.Text;
                selectedEmployee.LastName = txtLastName.Text;
                selectedEmployee.Position = txtPosition.Text;
                selectedEmployee.Phone = txtPhone.Text;
                selectedEmployee.Email = txtEmail.Text;
                empRepo.UpdateEmployee(selectedEmployee);
                LoadEmployees();
            }
            else
            {
                MessageBox.Show("Please select a employee to edit.");
            }
        }

        private void btnDelete_click(object sender, RoutedEventArgs e)
        {

            if (dgData.SelectedItem is Employee selectedEmployee)
            {
                var result = MessageBox.Show("Are you sure you want to delete this employee?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    empRepo.DeleteEmployee(selectedEmployee);
                    LoadEmployees();
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to delete.");
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadEmployees();
        }

        private void txtFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
