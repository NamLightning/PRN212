using BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IOrderRepository
    {
        void SaveOrder(Order Order);
        void DeleteOrder(Order Order);
        void UpdateOrder(Order Order);
        List<Order> GetOrders();
        Order GetOrderById(int id);
    }
}
