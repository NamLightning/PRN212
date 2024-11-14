using BusinessObjects.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO orderDetailDAO;

        public static OrderDetailDAO getInstance()
        {
            if (orderDetailDAO == null)
            {
                orderDetailDAO = new OrderDetailDAO();
            }
            return orderDetailDAO;
        }

        public List<OrderDetail> GetOrderDetails()
        {
            var listOrderDetail = new List<OrderDetail>();
            try
            {
                using var context = new FmartDbContext();
                listOrderDetail = context.OrderDetails.Include(c => c.OrderDetailId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listOrderDetail;
        }

        public void SaveOrderDetail(OrderDetail c)
        {
            try
            {
                using var context = new FmartDbContext();
                context.OrderDetails.Add(c);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateOrderDetail(OrderDetail OrderDetail)
        {
            try
            {
                using var context = new FmartDbContext();
                context.Entry<OrderDetail>(OrderDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteOrderDetail(OrderDetail OrderDetail)
        {
            try
            {
                using var context = new FmartDbContext();
                var c = context.OrderDetails.SingleOrDefault(c => c.OrderDetailId == OrderDetail.OrderDetailId);
                if (c != null)
                {
                    context.OrderDetails.Remove(c);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public OrderDetail? GetOrderDetailById(int id)
        {
            OrderDetail OrderDetail;

            try
            {
                using var context = new FmartDbContext();
                OrderDetail = context.OrderDetails.FirstOrDefault(e => e.OrderDetailId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the OrderDetail: " + ex.Message, ex);
            }
            return OrderDetail;
        }
    }
}
