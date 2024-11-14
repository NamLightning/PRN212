using BusinessObjects.Model;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void DeleteOrderDetail(OrderDetail OrderDetail) => OrderDetailDAO.getInstance().DeleteOrderDetail(OrderDetail);

        public OrderDetail GetOrderDetailById(int id) => OrderDetailDAO.getInstance().GetOrderDetailById(id);

        public List<OrderDetail> GetOrderDetails() => OrderDetailDAO.getInstance().GetOrderDetails();

        public void SaveOrderDetail(OrderDetail OrderDetail) => OrderDetailDAO.getInstance().SaveOrderDetail(OrderDetail);
        public void UpdateOrderDetail(OrderDetail OrderDetail) => OrderDetailDAO.getInstance().UpdateOrderDetail(OrderDetail);


    }
}
