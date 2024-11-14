using BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IOrderDetailRepository
    {
        void SaveOrderDetail(OrderDetail OrderDetail);
        void DeleteOrderDetail(OrderDetail OrderDetail);
        void UpdateOrderDetail(OrderDetail OrderDetail);
        List<OrderDetail> GetOrderDetails();
        OrderDetail GetOrderDetailById(int id);
    }
}
