using BazarCloth.DataBase;
using BazarCloth.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazarCloth.Services
{
    public class OrderService
    {
        public static OrderService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderService();
                }
                return instance;
            }
        }

        private static OrderService instance { get; set; }
        public OrderService()
        {

        }



        public List<Order> GetOrderList(string userID, string status, int pageNo, int pageSize)
        {
            using (CbContext _context = new CbContext())
            {
                var order = _context.Orders.ToList();
                if (!string.IsNullOrEmpty(userID))
                {
                    order = order.Where(x => x.UserId.ToLower().Contains(userID.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(status))
                {
                    order = order.Where(x => x.Status.ToLower().Contains(status.ToLower())).ToList();
                }

                return order.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public Order GetOrderByID(int id)
        {
            using (CbContext _context = new CbContext())
            {
                return _context.Orders.Where(x => x.Id.Equals(id)).Include(x => x.OrderItems).Include("OrderItems.Product").FirstOrDefault();
            }
        }

        public int SearchOrderCount(string userId, string status)
        {
            using (CbContext _context = new CbContext())
            {
                var order = _context.Orders.ToList();
                if (!string.IsNullOrEmpty(userId))
                {
                    order = order.Where(x => x.UserId.ToLower().Equals(userId.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(status))
                {
                    order = order.Where(x => x.Status.ToLower().Equals(status.ToLower())).ToList();
                }

                return order.Count();
            }

        }


        public bool UpdateOrderStatus(int id, string status)
        {
            using (CbContext _context = new CbContext())
            {
                var order = _context.Orders.Find(id);

                order.Status = status;

                _context.Entry(order).State = EntityState.Modified;

                return _context.SaveChanges()> 0;
            }
        }
    }
}
