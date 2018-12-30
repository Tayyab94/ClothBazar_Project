using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BazarCloth.Entities
{
   public class Product:BaseEntity
    {

        [Range(1,100,ErrorMessage ="Min length is 1 and Max is 100")]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Catagory Catagory { get; set; }
    }
}
