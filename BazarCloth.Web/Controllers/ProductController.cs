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
    public class ProductController : Controller
    {

        //ProductsService productsService = new ProductsService();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }


        //[HttpGet]
        public ActionResult ProductTable(string search, int? pageNo)
        {

            var pageSize = Project_ConfigurationServices.Instance.GetPageSize();
            ProductSearchViewModel model = new ProductSearchViewModel();

           pageNo = pageNo.HasValue ? pageNo.Value>0? pageNo.Value : 1:1;

            //This Conditaion is Save as abpve if Condition
            //if(pageNo.HasValue)
            //{
            //    if(pageNo.Value>0)
            //    {
            //        model.PageNo = pageNo.Value;
            //    }
            //    else
            //    {
            //        model.PageNo = 1;
            //    }
            //}
            //else
            //{
            //    model.PageNo = 1;
            //}

         //   model.Products = ProductsService.Instance.GetAllProducts(pageNo.Value);
            model.Products = ProductsService.Instance.GetAllProducts(search, pageNo.Value, pageSize);
            var totalRecords = ProductsService.Instance.GetProductCount(search);
           // model.Products = ProductsService.Instance.GetAllCatagories(search, pageNo.Value);


            if(model.Products!=null)
            {
                model.Pager = new Pager(totalRecords, pageNo, pageSize);


                return PartialView(model);
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }

            //if (String.IsNullOrEmpty(search) == false)
            //{
            //    model.Products = model.Products.Where(c => (c.Name != null && c.Name.ToLower().Contains(search))).ToList();
            //}

        
        }

        // GET: Catagory        
        [HttpGet]
        public ActionResult Create()
        {
            //CatagoriesService catagoriesService = new CatagoriesService();

            //this will gives us the List of the Catagory
            newProductViewModel model = new newProductViewModel();
            model.AvailableCategories = CatagoriesService.Instance.GetAllCatagories();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(newProductViewModel obj)
        {

            if(ModelState.IsValid)
            {
                Product product = new Product();
                product.Name = obj.Name; product.Price = obj.Price;
                product.Description = obj.Description;

                /* you can add the catagory's Id in two different ways 
                 1) Product.Catagory=CatagoryServices.catagoryByID(obj.CatagoryID); This funtion will retun the Catagory obje same
                    and then you dpn't need to set the Satate the of the catagory'obj  that is navigate 
                  */

                product.ImageUrl = obj.ImageUrl;
                product.Catagory = new Catagory { Id = obj.CatagoryId };

                ProductsService.Instance.SaveProduct(product);
                return RedirectToAction("ProductTable");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
            
        }

        // GET: Catagory        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            editProductViewModel model = new editProductViewModel();
            var product = ProductsService.Instance.GetProductById(id);

            model.Id = product.Id;
            model.Name = product.Name;
            model.Description = product.Description;
            model.ImageUrl = product.ImageUrl;
            model.Price = product.Price;
            model.CatagoryId = product.Catagory != null ? product.Catagory.Id : 0;
            model.AvailableCategories = CatagoriesService.Instance.GetAllCatagories();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(editProductViewModel product)
        {
       

            Product model = ProductsService.Instance.GetProductById(product.Id);

            model.Name = product.Name;
            model.Price = product.Price;
            model.Description = product.Description;
            model.Catagory = null;  //Mark is null, because it, because referencey key is cahnge 
            model.CategoryID = product.CatagoryId;

            //Don't Update the Image usrl if it is Empty 
            if(!string.IsNullOrEmpty(product.ImageUrl))
            {

                model.ImageUrl = product.ImageUrl;
            }
            ProductsService.Instance.UpdateProduct(model);
            return RedirectToAction("ProductTable");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            ProductsService.Instance.DeleteProduct(id);
            return RedirectToAction("ProductTable");
        }


        [HttpGet]
        public ActionResult Details(int id)
        {
            DetailProductViewModel model = new DetailProductViewModel();
            model.Product = ProductsService.Instance.GetProductById(id);
            return View(model);
        }
    }
}