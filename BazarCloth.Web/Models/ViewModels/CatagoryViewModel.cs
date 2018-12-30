using BazarCloth.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BazarCloth.Web.Models.ViewModels
{
    //public class CatagoryViewModel
    //{
    
    //}

    public class CategorySearchViewModel
    {
        public List<Catagory> Catagories { get; set; }

        public string SearchItem { get; set; }

        public Pager Pager { get; set; }
    }

    public class NewCatagoryViewModel
    {

        [Required]
        [StringLength(50, ErrorMessage = "Max Length is 50")]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }
        public bool isFeatured { get; set; }
    }

    public class EditCategoryViewModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
        public bool isFeatured { get; set; }
    }
}