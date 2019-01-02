using BazarCloth.DataBase;
using BazarCloth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazarCloth.Services
{
   public class ShopService
    {

        public static ShopService Instance
        {
            get
            {
                if (instance == null) instance = new ShopService();

                return instance;
            }
        }

        private  static ShopService instance { get; set; }

        public ShopService()
        {

        }


        //Returns the Total No or effected Rows
        public int SaveOrder(Order obj)
        {
            using (CbContext _context = new CbContext())
            {
                _context.Orders.Add(obj);

                return _context.SaveChanges();
            }
        }
    }
}
