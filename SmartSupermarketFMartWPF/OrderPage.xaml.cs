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
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        private IOrderRepository oRepo;
        private IOrderDetailRepository oDetailRepo;
        private IProductRepository productRepo;
        private ICustomerRepositories customerRepo;
        private List<OrderDetail> orderDetailList;
        private Customer customer;
        private Order order;
        public OrderPage()
        {
            InitializeComponent();
            oRepo = new OrderRepository();
            oDetailRepo = new OrderDetailRepository();
            productRepo = new ProductRepository();
            orderDetailList = new List<OrderDetail>();
            customerRepo = new CustomerRepository();
            customer = new Customer();
            order = new Order();
            LoadProductList();
            LoadOrderDetailList();

        }

        private void btnEdit_click(object sender, RoutedEventArgs e)
        {
            if (dgData.SelectedItem is OrderDetail selectedOrderDetail)
            {
                selectedOrderDetail.ProductId = Int32.Parse(cboProduct.SelectedValue.ToString());
                selectedOrderDetail.Quantity = Int32.Parse(txtQuantity.Text);

                oDetailRepo.UpdateOrderDetail(selectedOrderDetail);

                txtAmount_TotalAmount(sender, null);
                LoadOrderDetailList();
            }
            else
            {
                MessageBox.Show("Please select a item to edit.");
            }
        }

        private void LoadOrderDetailList()
        {
            try
            {
                orderDetailList= oDetailRepo.GetOrderDetails();
                dgData.ItemsSource = null;
                dgData.ItemsSource = orderDetailList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on load list of products");
            }
            finally
            {
                resetInput();
            }
        }
    

    private void btnDelete_click(object sender, RoutedEventArgs e)
        {
            if (dgData.SelectedItem is OrderDetail selectedOrderDetail)
            {
                var result = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    oDetailRepo.DeleteOrderDetail(selectedOrderDetail);

                    txtAmount_TotalAmount(sender, null);
                    LoadOrderDetailList();
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to delete.");
            }
        }

        private void btnCheckout_click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (txtFirstName == null || txtLastName == null || txtPhone == null)
                {
                    MessageBox.Show("One or more UI elements are not initialized.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtFirstName.Text))
                {
                    MessageBox.Show("Please enter a first name.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtLastName.Text))
                {
                    MessageBox.Show("Please enter a last name.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPhone.Text))
                {
                    MessageBox.Show("Please enter a phone.");
                    return;
                }

                customer.FirstName = txtFirstName.Text;
                customer.LastName = txtLastName.Text;
                customer.Phone = txtPhone.Text;
                customerRepo.UpdateCustomer(customer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.NavigationService.Navigate(new BillPage());
            }
        }

        private void btn_AddItemOrder(object sender, RoutedEventArgs e)
        {

            if (orderDetailList == null)
            {
                customer = new Customer();  
                customerRepo.SaveCustomer(customer); 

                order = new Order()
                {
                    CustomerId = customer.CustomerId,
                    OrderDate = DateTime.Now
                };
                oRepo.SaveOrder(order); 
            }

            OrderDetail orderDetail = new OrderDetail()
            {
                OrderId = order.OrderId,  
                ProductId = Int32.Parse(cboProduct.SelectedValue.ToString()),  
                Quantity = Int32.Parse(txtQuantity.Text.ToString())  
            };
            //order.TotalAmount = orderDetailList.Sum(item => item.PriceAtPurchase);
            oDetailRepo.SaveOrderDetail(orderDetail);

            txtAmount_TotalAmount(sender, null);
        }


        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedIndex == -1) return;

            DataGrid dataGrid = sender as DataGrid;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            if (row == null) return;

            DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
            if (RowColumn == null) return;

            string id = ((TextBlock)RowColumn.Content).Text;
            if (id == null) return;

            OrderDetail od = oDetailRepo.GetOrderDetailById(Int32.Parse(id));
            if (od == null) return;

            txtOrderDetailID.Text = od.OrderDetailId.ToString();
            cboProduct.SelectedValue = od.ProductId;
            txtQuantity.Text = od.Quantity.ToString();

        }

        private void txtAmount_TotalAmount(object sender, TextChangedEventArgs e)
        {
            decimal totalAmount = orderDetailList.Sum(item => item.PriceAtPurchase);
            txtAmount.Text = totalAmount.ToString("C");
            order.TotalAmount = totalAmount;
            oRepo.UpdateOrder(order);
        }

        private void resetInput()
        {
            cboProduct.SelectedValue = 0;
            txtPhone.Text = "";
            //txtFirstName.Text = "";
            //txtLastName.Text = "";
            //txtPhone.Text = "";
        }


        public void LoadProductList()
        {
            try
            {
                var catList = productRepo.GetProducts();
                cboProduct.ItemsSource = catList;
                cboProduct.DisplayMemberPath = "Name";
                cboProduct.SelectedValuePath = "ProductId";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on load list of products");
            }
        }

    }
}
