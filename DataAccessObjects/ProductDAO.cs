using BusinessObjects.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class ProductDAO
    {
        private static ProductDAO productDAO;

        public static ProductDAO getInstance()
        {
            if (productDAO == null)
            {
                productDAO = new ProductDAO();
            }
            return productDAO;
        }

        public List<Product> GetProducts()
        {
            var listProducts = new List<Product>();
            try
            {
                using var context = new FmartDbContext();
                listProducts = context.Products.Include(c => c.OrderDetails).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listProducts;
        }

        public void SaveProduct(Product c)
        {
            try
            {
                using var context = new FmartDbContext();
                context.Products.Add(c);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateProduct(Product Product)
        {
            try
            {
                using var context = new FmartDbContext();
                context.Entry<Product>(Product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteProduct(Product Product)
        {
            try
            {
                using var context = new FmartDbContext();
                var c = context.Products.SingleOrDefault(c => c.ProductId == Product.ProductId);
                if (c != null)
                {
                    context.Products.Remove(c);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public Product? GetProductById(int id)
        //{
        //    Product Product;

        //    try
        //    {
        //        using var context = new FmartDbContext();
        //        Product = context.Products.FirstOrDefault(e => e.ProductId == id);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("An error occurred while retrieving the Product: " + ex.Message, ex);
        //    }
        //    return Product;
        //}


    }
}
