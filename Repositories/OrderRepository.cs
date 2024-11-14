using BusinessObjects.Model;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public void DeleteOrder(Order Order) => OrderDAO.getInstance().DeleteOrder(Order);

        public Order GetOrderById(int id) => OrderDAO.getInstance().GetOrderById(id);

        public List<Order> GetOrders() => OrderDAO.getInstance().GetOrders();

        public void SaveOrder(Order Order) => OrderDAO.getInstance().SaveOrder(Order);
        public void UpdateOrder(Order Order) => OrderDAO.getInstance().UpdateOrder(Order);

    }
}
