using BazarCloth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BazarCloth.Web.Models.ViewModels
{
    public class CheckOutViewModel
    {

        public List<Product> carProductList { get; set; }

        public List<int> CarProductIDs { get; set; }


      public ApplicationUser Users { get; set; }
    }

   public class ShopGalleryViewModel
    {
        public int MaximumPrice { get; set; }

        public string searchITem { get; set; }
        public List<Product> Products { get; set; }

        public int? SortBy { get; set; }

        public int? cataId { get; set; }
        public List<Catagory>  FeaturedCatagories { get; set; }

        public Pager Pager { get; set; }
    }

    public class ShopGalleryFilterViewModel
    {
        public int? cataId { get; set; }
        public string searchITem { get; set; }

        public List<Product> Products { get; set; }

        public Pager Pager { get; set; }
        public int? SortBy { get;  set; }
    }
}