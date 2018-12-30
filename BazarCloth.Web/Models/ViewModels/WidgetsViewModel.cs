using BazarCloth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BazarCloth.Web.Models.ViewModels
{
    public class ProdcutListViewNodel
    {
        public List<Product> Products { get; set; }

        public bool isLastetProduct { get; set;
        }

        public int cata_Id { get; set; }
    }
}