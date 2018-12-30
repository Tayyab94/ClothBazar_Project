using BazarCloth.Services;
using BazarCloth.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BazarCloth.Web.Controllers
{
    public class WidgetsController : Controller
    {
        // GET: Widgets
        public ActionResult Products(bool isLastetProducts,int? catId = 0)
        {

            ProdcutListViewNodel model = new ProdcutListViewNodel();
            model.isLastetProduct = isLastetProducts;
            model.cata_Id = catId.Value;
            if (isLastetProducts)
            {
                model.Products = ProductsService.Instance.GetLatestProducts(4);
            }
            else if(isLastetProducts && catId.HasValue && catId.Value >0)
            {
                model.Products = ProductsService.Instance.GetRelatedProducts(catId.Value, 4);
            }
            else
            {
                model.Products = ProductsService.Instance.GetAllProducts(1, 8);
            }
            return PartialView("_Products",model);
        }
    }
}