using BazarCloth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BazarCloth.Web.Models.ViewModels
{
    public class HomeViewModel
    {
       public List<Catagory> FeaturedCatagories { get; set; }
    }
}