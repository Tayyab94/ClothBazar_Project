using BazarCloth.Services;
using BazarCloth.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BazarCloth.Web.Controllers
{
    public class ShopController : Controller
    {


        //ProductsService productsService = new ProductsService();
        // GET: Shop
        public ActionResult CheckOut()
        {
            var GetProductId = Request.Cookies["CartProduct"];

            CheckOutViewModel model = new CheckOutViewModel();

            if (GetProductId != null)
            {
                var path = GetProductId.Path;

                //var ProductId = GetProductId.Value;

                //var ids = ProductId.Split('-');

                //List<int> PIDs = ids.Select(x => int.Parse(x)).ToList();

                model.CarProductIDs = GetProductId.Value.Split('-').Select(c => int.Parse(c)).ToList();


                model.carProductList = ProductsService.Instance.GetAllProducts(model.CarProductIDs);
            }
            return View(model);
        }

        public ActionResult Index(string sreachProduct, int? MinimumPrice, int? MaximumPrice, int? CataId, int? SortBy, int? pageNo)
        {
            var pageSize = Project_ConfigurationServices.Instance.GetPageSizeForShopPage(); 

            ShopGalleryViewModel model = new ShopGalleryViewModel();
            model.FeaturedCatagories = CatagoriesService.Instance.GetFeaturedCatagoriesForShopPage();
            if (SortBy.HasValue)
            {
                model.SortBy = SortBy.Value;
            }

            if (CataId.HasValue)
            {
                model.cataId = CataId.Value;
            }

            model.MaximumPrice = ProductsService.Instance.GetMaximumPrice();

            pageNo = pageNo.HasValue ? pageNo.Value > 1 ? pageNo.Value : 1 : 1;
            model.Products = ProductsService.Instance.GetShowAllProducts(sreachProduct, MinimumPrice, MaximumPrice, CataId, SortBy, pageNo.Value, pageSize);

            int TotalItemCount = ProductsService.Instance.GetAllProductsCount(sreachProduct, MinimumPrice, MaximumPrice, CataId, SortBy);


            model.Pager = new Pager(TotalItemCount, pageNo, pageSize);

            return View(model);
        }

        public ActionResult FilterSearch(string sreachProduct, int? MinimumPrice, int? MaximumPrice, int? CataId, int? SortBy, int? pageNo)
        {
            var pageSize = Project_ConfigurationServices.Instance.GetPageSizeForShopPage();
            ShopGalleryFilterViewModel model = new ShopGalleryFilterViewModel();


            pageNo = pageNo.HasValue ? pageNo.Value > 1 ? pageNo.Value : 1 : 1;

            if (SortBy.HasValue)
            {
                model.SortBy = SortBy.Value;
            }

            if (CataId.HasValue)
            {
                model.cataId = CataId.Value;
            }
            model.Products = ProductsService.Instance.GetShowAllProducts(sreachProduct, MinimumPrice, MaximumPrice, CataId, SortBy, pageNo.Value, pageSize);

            int TotalItemCount = ProductsService.Instance.GetAllProductsCount(sreachProduct, MinimumPrice, MaximumPrice, CataId, SortBy);

            model.Pager = new Pager(TotalItemCount, pageNo, pageSize);

            return PartialView(model);
        }
    }
}