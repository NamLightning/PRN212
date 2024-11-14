using BusinessObjects.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class OrderDAO
    {
        private static OrderDAO orderDAO;

        public static OrderDAO getInstance()
        {
            if (orderDAO == null)
            {
                orderDAO = new OrderDAO();
            }
            return orderDAO;
        }

        public List<Order> GetOrders()
        {
            var listOrder = new List<Order>();
            try
            {
                using var context = new FmartDbContext();
                listOrder = context.Orders.Include(c => c.OrderDetails).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listOrder;
        }

        public void SaveOrder(Order c)
        {
            try
            {
                using var context = new FmartDbContext();
                context.Orders.Add(c);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateOrder(Order Order)
        {
            try
            {
                using var context = new FmartDbContext();
                context.Entry<Order>(Order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteOrder(Order Order)
        {
            try
            {
                using var context = new FmartDbContext();
                var c = context.Orders.SingleOrDefault(c => c.OrderId == Order.OrderId);
                if (c != null)
                {
                    context.Orders.Remove(c);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Order? GetOrderById(int id)
        {
            Order Order;

            try
            {
                using var context = new FmartDbContext();
                Order = context.Orders.FirstOrDefault(e => e.OrderId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the Order: " + ex.Message, ex);
            }
            return Order;
        }
    }
}
