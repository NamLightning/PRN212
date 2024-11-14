using BusinessObjects.Model;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_InvManagement_Click(object sender, RoutedEventArgs e)
        {
            frMain.Content = new InventoryManagementPage();
        }

        private void btn_CusManagement_Click(object sender, RoutedEventArgs e)
        {
            frMain.Content = new CustomerManagementPage();
        }

        private void btn_EmpManagement_Click(object sender, RoutedEventArgs e)
        {
            frMain.Content = new EmployeeManagementPage();
        }

<<<<<<< HEAD
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
=======
        private void btn_MonthlyReports_Click(object sender, RoutedEventArgs e)
        {
            frMain.Content = new MonthlyReportsPage();
>>>>>>> ac8f8cf0aee117a07ef59e743d97b9568cfa9253
        }
    }
}