using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazarCloth.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime OrderAt { get; set; }

        public string Status { get; set; }

        public decimal TotalBill { get; set; }

        public virtual List<OrderItems> OrderItems { get; set; }


    }
}
