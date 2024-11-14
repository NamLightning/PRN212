using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace SmartSupermarketFMartWPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        string AdminUsername;
        string AdminPassword;
        public LoginWindow()
        {
            InitializeComponent();
            AdminUsername = GetConfiguration()["AdminCredentials:Username"];
            AdminPassword = GetConfiguration()["AdminCredentials:Password"];
        }

        private IConfiguration GetConfiguration()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", false, true).Build();
            return configuration;
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            if (username == AdminUsername && password == AdminPassword)
            {
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
                mainWindow.Closed += (s, e) => mainWindow.Close();
            }
            else
            {
                MessageBox.Show("You have no permission to access this function!");
            }
        }

        private void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Password = "";
        }
    }
}
