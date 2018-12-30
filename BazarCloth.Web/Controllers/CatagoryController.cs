using BazarCloth.Entities;
using BazarCloth.Services;
using BazarCloth.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BazarCloth.Web.Controllers
{
    public class CatagoryController : Controller
    {

        //CatagoriesService catagoriesService = new CatagoriesService();

        [HttpGet]
        public ActionResult Index()
        {
            var catagoryList = CatagoriesService.Instance.GetAllCatagories();
            return View(catagoryList);
        }

        public ActionResult CatagoryTable(string search, int? pageNo)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();
            model.SearchItem = search;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;


            var totalRecords = CatagoriesService.Instance.GetCatagoryCount(search);
                model.Catagories = CatagoriesService.Instance.GetAllCatagories(search,pageNo.Value);


            if(model.Catagories!=null)
            {
                model.Pager = new Pager(totalRecords, pageNo, 3);
                return PartialView("_CatagoryTable", model);
            }
            else
            {
                return HttpNotFound();
            }
            //if(!string.IsNullOrEmpty(search))
            //{
               

            //    model.Catagories = model.Catagories.Where(x => x.Name != null && x.Name.ToLower().Contains(model.SearchItem.ToLower())).ToList();
            //}
            
        }

        // GET: Catagory
        [HttpGet]
        public ActionResult Create()
        {
            NewCatagoryViewModel model = new NewCatagoryViewModel();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(NewCatagoryViewModel catagory)
        {
            if (ModelState.IsValid)
            {
                var newCatagory = new Catagory();
                newCatagory.Name = catagory.Name;
                newCatagory.Description = catagory.Description;
                newCatagory.ImageUrl = catagory.ImageUrl;
                newCatagory.isFeatured = catagory.isFeatured;
                CatagoriesService.Instance.SaveCatagory(newCatagory);


                return RedirectToAction("CatagoryTable");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
          
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();
            var catagoryList = CatagoriesService.Instance.GetCatagoryById(id);

            model.Id = catagoryList.Id;
            model.Name = catagoryList.Name;
            model.Description = catagoryList.Description;
            model.ImageUrl = catagoryList.ImageUrl;
            model.isFeatured = catagoryList.isFeatured;
            return PartialView(model);
         
        }

        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel catagory)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();
            var catagoryList = CatagoriesService.Instance.GetCatagoryById(catagory.Id);

            catagoryList.Name = catagory.Name;
            catagoryList.Description = catagory.Description;
            catagoryList.ImageUrl = catagory.ImageUrl;
            catagoryList.isFeatured = catagory.isFeatured;
            CatagoriesService.Instance.UpdateCatagory(catagoryList);

            return RedirectToAction("CatagoryTable");
        }



        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    var catagoryList = catagoriesService.GetCatagoryById(id);
        //    return View(catagoryList);

        //}

        [HttpPost]
        public ActionResult Delete(int  id)
        {
            CatagoriesService.Instance.DeleteCatagory(id);

            return RedirectToAction("CatagoryTable");
        }
    }
}