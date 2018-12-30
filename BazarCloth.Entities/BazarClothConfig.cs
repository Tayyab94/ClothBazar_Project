using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazarCloth.Entities
{
    public class BazarClothConfig
    {
        [Key]
        public string SKey { get; set; }

        public string SValue { get; set; }
    }
}
