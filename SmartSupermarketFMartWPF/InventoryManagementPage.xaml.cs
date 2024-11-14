using BusinessObjects.Model;
using Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                ProductImageDisplay.Source = DisplayImage(selectedProduct.ProductImage);
            }
        }

        public void ClearInput()
        {
            txtProductId.Text = "";
            txtProductName.Text = "";
            txtCategory.Text = "";
            txtPrice.Text = "";
            txtStock.Text = "";
            txtExpiryDate.Text = "";
            ProductImageDisplay.Source = null;
        }

        private void LoadProducts()
        {
            try
            {
                ProductListView.SelectedItem = null;
                ProductListView.ItemsSource = productRepository.GetProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                         : (DateOnly?)null,
                    ProductImage = GetImageFromDisplay()
                };

                productRepository.SaveProduct(newProduct);
                LoadProducts();
                ClearInput();
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
                    if (GetImageFromDisplay() != null)
                    {
                        selectedProduct.ProductImage = GetImageFromDisplay();
                    }
                    productRepository.UpdateProduct(selectedProduct);
                    LoadProducts();
                    ClearInput();
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
                    ClearInput();
                }
            }
            else
            {
                MessageBox.Show("Please select a product to delete.");
            }
        }

        private byte[] ConvertImageToByteArray(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        private void UploadImage_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg;*.jpeg;*.png"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                var imageBytes = ConvertImageToByteArray(filePath);

                ProductImageDisplay.Source = DisplayImage(imageBytes);
            }
        }

        private BitmapImage DisplayImage(byte[] imageBytes)
        {
            if (imageBytes != null && imageBytes.Length > 0)
            {
                using (var ms = new MemoryStream(imageBytes))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                    return image;
                }
            }
            return null;
        }

        private byte[] GetImageFromDisplay()
        {
            if (ProductImageDisplay.Source is BitmapSource bitmapSource)
            {
                using (var ms = new MemoryStream())
                {
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                    encoder.Save(ms);
                    return ms.ToArray();
                }
            }
            return null;
        }
    }

    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] imageData && imageData.Length > 0)
            {
                var image = new BitmapImage();
                using (var ms = new MemoryStream(imageData))
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                }
                return image;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
