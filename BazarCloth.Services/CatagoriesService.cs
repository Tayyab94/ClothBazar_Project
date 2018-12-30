using BazarCloth.DataBase;
using BazarCloth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace BazarCloth.Services
{
    public class CatagoriesService
    {


        #region Apply_SingleTon
        public static CatagoriesService Instance { get {
                if (instance == null)
                    instance = new CatagoriesService();
                return instance;
            } }


        private static CatagoriesService instance { get; set; }

        private CatagoriesService()
        {

        }

        #endregion
        //Show all the Catagories list
        public List<Catagory> GetAllCatagories()
        {
            using (var _context = new CbContext())
            {

                return _context.Catagories.Include(x=>x.Products).ToList();
            }
        }

        //Show all the Catagories list
        public List<Catagory> GetAllCatagories(string search, int page)
        {
            int pageSize = 3;
            using (var _context = new CbContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return _context.Catagories.Where(x => x.Name.ToLower().Contains(search.ToLower()) && x.Name != null)
                        .OrderBy(x=>x.Id).Skip((page-1)*pageSize).Take(pageSize).Include(x=>x.Products)
                        .ToList();
                }
                else
                {
                    return _context.Catagories
                        .OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).Include(x => x.Products)
                        .ToList();
                }
                
            }
        }

        public int GetCatagoryCount(string search)
        {
            using (CbContext _context = new CbContext())
            {
                if(!string.IsNullOrEmpty(search))
                {
                    return _context.Catagories.Where(x => x.Name.ToLower().Contains(search.ToLower()) && x.Name != null).Count();
                }
                else
                {
                    return _context.Catagories.Count();
                }
            }
        }

        //Show all the Featured Catagories list
        public List<Catagory> GetFeaturedCatagories()
        {
            using (var _context = new CbContext())
            {

                return _context.Catagories.Where(x => x.isFeatured && x.ImageUrl != null).OrderByDescending(x=>x.Id).Take(4).ToList();
            }
        }
        //Show all the Featured Catagories list
        public List<Catagory> GetFeaturedCatagoriesForShopPage()
        {
            using (var _context = new CbContext())
            {

                return _context.Catagories.Where(x => x.isFeatured && x.ImageUrl != null).OrderByDescending(x => x.Id).ToList();
            }
        }
        //Return the only one Catagory By Id
        public Catagory GetCatagoryById(int id)
        {
            using (var _context = new CbContext())
            {

                return _context.Catagories.Find(id);
            }
        }


        //Funtion Save the new Catagory
        public void SaveCatagory(Catagory catagory)
        {
            using(var _context= new CbContext())
            {
                _context.Catagories.Add(catagory);
                _context.SaveChanges();
            }
        }

        //Updata Catagory Function
        public void UpdateCatagory(Catagory catagory)
        {
            using (var _context = new CbContext())
            {
                //Catagory obj = _context.Catagories.Find(catagory.Id);
                

                //if(catagory.ImageUrl==null)
                //{

                //    obj.Name = catagory.Name;
                //    obj.Products = catagory.Products;
                //    obj.Description = catagory.Description;
                //    obj.isFeatured = catagory.isFeatured;
                   
                //}
                //else
                //{

                    
                //}

                _context.Entry(catagory).State = System.Data.Entity.EntityState.Modified;
                //_context.Catagories.Add(catagory);
                _context.SaveChanges();
            }
        }

        //Delete Catagory Function
        public void DeleteCatagory(int id)
        {
            using (var _context = new CbContext())
            {

                //_context.Entry(catagory).State = System.Data.Entity.EntityState.Modified;
                var ca = _context.Catagories.Include(x=>x.Products).SingleOrDefault(x=>x.Id==id);
                _context.Products.RemoveRange(ca.Products);
                _context.Catagories.Remove(ca);
                _context.SaveChanges();
            }
        }
    }
}
