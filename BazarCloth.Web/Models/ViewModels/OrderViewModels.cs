using BazarCloth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BazarCloth.Web.Models.ViewModels
{
    public class OrderViewModel
    {
        public List<Order> Orders { get; set; }
        public string status { get; set; }
        public string userID { get; set; }

        public Pager Pager { get; set; }
    }


    public class OrderDetailViewModel
    {
        public Order Orders { get; set; }

        public ApplicationUser User { get; set; }

        public List<string> AvailableStatus { get; set; }
    }
}