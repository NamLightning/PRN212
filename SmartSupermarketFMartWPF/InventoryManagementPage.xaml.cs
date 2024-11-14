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
    /// Interaction logic for InventoryManagementPage.xaml
    /// </summary>
    public partial class InventoryManagementPage : Page
    {
        private ProductRepository productRepository = new ProductRepository();

        public InventoryManagementPage()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductListView.SelectedItem is Product selectedProduct)
            {
                txtProductId.Text = selectedProduct.ProductId.ToString();
                txtProductName.Text = selectedProduct.Name;
                txtCategory.Text = selectedProduct.Category;
                txtPrice.Text = selectedProduct.Price.ToString();
                txtStock.Text = selectedProduct.StockQuantity.ToString();
                txtExpiryDate.Text = selectedProduct.ExpiryDate?.ToString("yyyy-MM-dd");
            }
        }

        private void LoadProducts()
        {
            ProductListView.SelectedItem = null;
            ProductListView.ItemsSource = productRepository.GetProducts();
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var newProduct = new Product
                {
                    Name = txtProductName.Text,
                    Category = txtCategory.Text,
                    Price = decimal.Parse(txtPrice.Text),
                    StockQuantity = int.Parse(txtStock.Text),
                    ExpiryDate = DateTime.TryParse(txtExpiryDate.Text, out var expiryDate)
                         ? DateOnly.FromDateTime(expiryDate)
                         : (DateOnly?)null
                };

                productRepository.SaveProduct(newProduct);
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating product: " + ex.Message);
            }
        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            if (ProductListView.SelectedItem is Product selectedProduct)
            {
                try
                {
                    selectedProduct.Name = txtProductName.Text;
                    selectedProduct.Category = txtCategory.Text;
                    selectedProduct.Price = decimal.Parse(txtPrice.Text);
                    selectedProduct.StockQuantity = int.Parse(txtStock.Text);
                    selectedProduct.ExpiryDate = DateTime.TryParse(txtExpiryDate.Text, out var expiryDate)
                         ? DateOnly.FromDateTime(expiryDate)
                         : (DateOnly?)null;

                    productRepository.UpdateProduct(selectedProduct);
                    LoadProducts();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating product: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a product to edit.");
            }
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if (ProductListView.SelectedItem is Product selectedProduct)
            {
                var result = MessageBox.Show("Are you sure you want to delete this product?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    productRepository.DeleteProduct(selectedProduct);
                    LoadProducts();
                }
            }
            else
            {
                MessageBox.Show("Please select a product to delete.");
            }
        }
        private void BackToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to MainWindow.xaml
            var mainWindow = new MainWindow();
            mainWindow.Show();
            var currentWindow = Window.GetWindow(this);
            currentWindow?.Close();
        }
    }
}
