using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazarCloth.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50,ErrorMessage ="Max Length is 50")]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }
    }
}
