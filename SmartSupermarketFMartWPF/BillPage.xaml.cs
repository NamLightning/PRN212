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
    /// Interaction logic for BillPage.xaml
    /// </summary>
    public partial class BillPage : Page
    {
        private IOrderDetailRepository oDetailRepo;
        private List<OrderDetail> orderDetailList;


        public BillPage()
        {
            InitializeComponent();
            LoadOrderDetailList();
            oDetailRepo = new OrderDetailRepository();
            orderDetailList = new List<OrderDetail>();

        }

        private void btnReturn_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new OrderPage());
        }

        private void LoadOrderDetailList()
        {
            try
            {
                orderDetailList = oDetailRepo.GetOrderDetails();
                dgData.ItemsSource = null;
                dgData.ItemsSource = orderDetailList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on load list of products");
            }
        }

        private void txtAmount_TotalAmount(object sender, TextChangedEventArgs e)
        {
            decimal totalAmount = orderDetailList.Sum(item => item.PriceAtPurchase);
            txtAmount.Text = totalAmount.ToString("C");
        }

    }
}
