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
    public class OrderController : Controller
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


        // GET: Order
        public ActionResult Index(string userID, string status, int? pageNo)
        {
            OrderViewModel model = new OrderViewModel();
            model.userID = userID;
            model.status = status;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo : 1 : 1;

            var pageSize = Project_ConfigurationServices.Instance.GetPageSize();

            model.Orders = OrderService.Instance.GetOrderList(userID, status, pageNo.Value, pageSize);

            var totalCount = OrderService.Instance.SearchOrderCount(userID, status);
            model.Pager = new Pager(totalCount, pageNo, pageSize);
            return View(model);
        }


        public ActionResult Details(int id)
        {
            OrderDetailViewModel model = new OrderDetailViewModel();

            model.Orders = OrderService.Instance.GetOrderByID(id);

            model.User = UserManager.FindById(model.Orders.UserId);

            model.AvailableStatus = new List<string>() { "Pendding", "In Progess", "Delivered" };
            return View(model);
        }


        public JsonResult ChangeStatus(string status,int id)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            result.Data = new { Success = OrderService.Instance.UpdateOrderStatus(id, status) };

            return result;
        }
    }
}