using BazarCloth.Services;
using BazarCloth.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BazarCloth.Web.Controllers
{
    public class HomeController : Controller
    {
        //CatagoriesService catagoriesService = new CatagoriesService();
        public ActionResult Index()
        {
            HomeViewModel homeView = new HomeViewModel();

       homeView.FeaturedCatagories = CatagoriesService.Instance.GetFeaturedCatagories();
          
         
            return View(homeView);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult service()
        {
            return View();
        }
    }
}