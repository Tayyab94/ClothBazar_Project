using BazarCloth.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazarCloth.DataBase
{
    public class CbContext:DbContext
    {

        public CbContext():base("BazarClothConnection")
        {

        }

        public DbSet<Catagory> Catagories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<BazarClothConfig>  bazarClothConfigs { get; set; }
        
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItems> OrderItems { get; set; }
    }
}
