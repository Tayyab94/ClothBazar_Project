using BazarCloth.Entities;
using BazarCloth.Services;
using BazarCloth.Web.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BazarCloth.Web.Controllers
{
    public class ShopController : Controller
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

 

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        //ProductsService productsService = new ProductsService();
        // GET: Shop

       [Authorize]
        public ActionResult CheckOut()
        {
            var GetProductId = Request.Cookies["CartProduct"];

            CheckOutViewModel model = new CheckOutViewModel();

            if (GetProductId != null && !string.IsNullOrEmpty(GetProductId.Value))
            {
                var path = GetProductId.Path;

                //var ProductId = GetProductId.Value;

                //var ids = ProductId.Split('-');

                //List<int> PIDs = ids.Select(x => int.Parse(x)).ToList();

                model.CarProductIDs = GetProductId.Value.Split('-').Select(c => int.Parse(c)).ToList();


                model.carProductList = ProductsService.Instance.GetAllProducts(model.CarProductIDs);

                model.Users = UserManager.FindById(User.Identity.GetUserId());
            }
            return View(model);
        }

        public ActionResult Index(string sreachProduct, int? MinimumPrice, int? MaximumPrice, int? CataId, int? SortBy, int? pageNo)
        {
            var pageSize = Project_ConfigurationServices.Instance.GetPageSizeForShopPage(); 

            ShopGalleryViewModel model = new ShopGalleryViewModel();

            model.FeaturedCatagories = CatagoriesService.Instance.GetFeaturedCatagoriesForShopPage();

            model.searchITem = sreachProduct;
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
            model.searchITem = sreachProduct;
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

        [HttpGet]
        public JsonResult PlaceOrder(string ProductsID) 
        {

            //idr ana chahiay ye idr aa k checkoout  py cahla jata hai

            //JsonResult result = new JsonResult();
            //result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            //if(string.IsNullOrEmpty(ProductsID))
            //{
            //    result.Data = new { Success = false };
            //}
            //else
            //{
                var productQuanity = ProductsID.Split('-').Select(x => int.Parse(x)).ToList();

                var boughtProductsList = ProductsService.Instance.GetAllProducts(productQuanity.Distinct().ToList());

                Order order = new Order();

                order.UserId = User.Identity.GetUserId();

                order.OrderAt = DateTime.Now;

                order.Status = "Pendding";

                order.TotalBill = boughtProductsList.Sum(x => x.Price * productQuanity.Where(ProductID => ProductID == x.Id).Count());

                order.OrderItems = new List<OrderItems>();
                order.OrderItems.AddRange(boughtProductsList.Select(x => new OrderItems() { ProductId = x.Id, Quantity = productQuanity.Where(productId => productId == x.Id).Count() }));

                var effectedRow = ShopService.Instance.SaveOrder(order);

                //result.Data = new { Success = true, Rows = effectedRow };

              
            //}
            return Json(new { Success = true, Rows = effectedRow },JsonRequestBehavior.AllowGet);
        }
    }
}