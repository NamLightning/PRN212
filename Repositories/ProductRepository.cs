using BusinessObjects.Model;
using DataAccessObjects;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {

        public void DeleteProduct(Product customer)
        => ProductDAO.getInstance().DeleteProduct(customer);

        public List<Product> GetProducts()
        => ProductDAO.getInstance().GetProducts();

        public void SaveProduct(Product customer)
        => ProductDAO.getInstance().SaveProduct(customer);

        public void UpdateProduct(Product customer)
        => ProductDAO.getInstance().UpdateProduct(customer);
    }
}
