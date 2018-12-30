﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazarCloth.Entities
{
   public class Catagory:BaseEntity
    {
        public string ImageUrl { get; set; }
        public List<Product> Products { get; set; }

        public bool isFeatured { get; set; }
    }
}
