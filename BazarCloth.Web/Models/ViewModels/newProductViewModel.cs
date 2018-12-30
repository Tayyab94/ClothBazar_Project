using BazarCloth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BazarCloth.Web.Models.ViewModels
{

    public class ProductSearchViewModel
    {

        public List<Product> Products { get; set; }

        public string SearchItem { get; set; }
        public int PageNo { get; internal set; }

        public Pager Pager { get; set; }
    }
    public class newProductViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
        public int CatagoryId { get; set; }


        public List<Catagory> AvailableCategories { get; set; }
    }

    public class editProductViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
        public int CatagoryId { get; set; }


        public List<Catagory> AvailableCategories { get; set; }

    }

    public class DetailProductViewModel
    {

        public Product Product { get; set; }

    }
}