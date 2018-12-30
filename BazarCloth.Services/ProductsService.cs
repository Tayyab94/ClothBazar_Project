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
    public class ProductsService:IDisposable
    {

        #region Apply_SingleTon

        public static ProductsService Instance { get {
                if (instance == null)
                    instance = new ProductsService();

                return instance;
            } }


        private static ProductsService instance { get; set; }


        private ProductsService()
        {
           
        }

        #endregion



        //Show all the Product list
        public List<Product> GetAllProducts(int pageNo)
        {
            int pageSize = 10;
            using (var _context = new CbContext())
            {
                //this locig is for paging
                return _context.Products.OrderBy(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Catagory).ToList();
     
                //return _context.Products.Include(x=>x.Catagory).ToList();
            }
        }

        //Show all the Product list
        public List<Product> GetAllProducts(string search,int pageNo,int pageSize)
        {
            using (var _context = new CbContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return _context.Products.Where(x=>x.Name.ToLower().Contains(search.ToLower())).OrderBy(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Catagory).ToList();

                }
                else
                {
                    return _context.Products.OrderBy(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Catagory).ToList();

                }

            }
        }

        //Returnt the Prpoduct Count

        public int GetProductCount(string search)
        {
            using (CbContext _context = new CbContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return _context.Products.Where(x => x.Name.ToLower().Contains(search.ToLower()) && x.Name != null).Count();
                }
                else
                {
                    return _context.Products.Count();
                }
            }
        }

        public List<Product> GetAllProducts(List<int>Id)
        {
            using (var _context = new CbContext())
            {

                return _context.Products.Where(x => Id.Contains(x.Id)).ToList();
            }
        }

        public int GetMaximumPrice()
        {
            using (var _context = new CbContext())
            {

                return (int)(_context.Products.Max(x => x.Price));
            }
        }

        //this function is show in shop Controller
        public List<Product> GetShowAllProducts(string sreachProduct, int? minimumPrice, int? maximumPrice, int?cataId,int? SortBy,int pageNo,int pageSize)
        {
            using (var _context = new CbContext())
            {

                List<Product> productsList = _context.Products.ToList();

                if(cataId.HasValue)
                {
                    productsList = productsList.Where(x => x.Catagory.Id == cataId.Value).ToList();
                }

                if(!string.IsNullOrEmpty(sreachProduct))
                {
                    productsList = productsList.Where(x => x.Name.ToLower().Contains(sreachProduct.ToLower())).ToList();
                }

                if(minimumPrice.HasValue)
                {
                    productsList = productsList.Where(x => x.Price >= minimumPrice).ToList();
                }
                if(maximumPrice.HasValue)
                {
                    productsList = productsList.Where(x => x.Price <= maximumPrice).ToList();
                }

                if(SortBy.HasValue)
                {
                    switch ((SortByEnum)SortBy.Value)
                    {
                      
                        case SortByEnum.Popularity:

                            break;
                        case SortByEnum.lowToHigh:
                            productsList = productsList.OrderBy(x => x.Price).ToList();
                            break;
                        case SortByEnum.highToLow:
                            productsList = productsList.OrderByDescending(x => x.Price).ToList();
                            break;
                        default:
                            productsList = productsList.ToList();
                            break;
                    }
                }

                return productsList.Skip((pageNo-1)*pageSize).Take(pageSize).ToList();
            }

        }



        //this function is Returnt the Total product Count
        public int GetAllProductsCount(string sreachProduct, int? minimumPrice, int? maximumPrice, int? cataId, int? SortBy)
        {
            using (var _context = new CbContext())
            {

                List<Product> productsList = _context.Products.ToList();

                if (cataId.HasValue)
                {
                    productsList = productsList.Where(x => x.Catagory.Id == cataId.Value).ToList();
                }

                if (!string.IsNullOrEmpty(sreachProduct))
                {
                    productsList = productsList.Where(x => x.Name.ToLower().Contains(sreachProduct.ToLower())).ToList();
                }

                if (minimumPrice.HasValue)
                {
                    productsList = productsList.Where(x => x.Price >= minimumPrice).ToList();
                }
                if (maximumPrice.HasValue)
                {
                    productsList = productsList.Where(x => x.Price <= maximumPrice).ToList();
                }

                if (SortBy.HasValue)
                {
                    switch ((SortByEnum)SortBy.Value)
                    {

                        case SortByEnum.Popularity:

                            break;
                        case SortByEnum.lowToHigh:
                            productsList = productsList.OrderBy(x => x.Price).ToList();
                            break;
                        case SortByEnum.highToLow:
                            productsList = productsList.OrderByDescending(x => x.Price).ToList();
                            break;
                        default:
                            productsList = productsList.ToList();
                            break;
                    }
                }

                return productsList.Count;
            }

        }

        //Show all the Product list
        public List<Product> GetLatestProducts(int numberOfProducts)
        {
            
            using (var _context = new CbContext())
            {
                //this locig is for paging
                return _context.Products.Take(numberOfProducts).Include(x => x.Catagory).OrderByDescending(x=>x.Id).ToList();

                //return _context.Products.Include(x=>x.Catagory).ToList();
            }
        }

        //Show all the Product list
        public List<Product> GetAllProducts(int pageNo, int pageSize)
        {
        
            using (var _context = new CbContext())
            {
                //this locig is for paging
                return _context.Products.OrderBy(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Catagory).ToList();

                //return _context.Products.Include(x=>x.Catagory).ToList();
            }
        }


        //Show Related the Product list
        public List<Product> GetRelatedProducts(int ID, int pageSize)
        {

            using (var _context = new CbContext())
            {
                //this locig is for paging
                return _context.Products.Where(x=>x.Catagory.Id==ID)
                    .OrderBy(x => x.Id).Take(pageSize).Include(x => x.Catagory).ToList();

                //return _context.Products.Include(x=>x.Catagory).ToList();
            }
        }

        //Return the only one Product By Id
        public Product GetProductById(int id)
        {
            using (var _context = new CbContext())
            {

                return _context.Products.Include(x=>x.Catagory).FirstOrDefault(x=>x.Id==id);
            }
        }


        //Funtion Save the new Product
        public void SaveProduct(Product product)
        {
            using(var _context= new CbContext())
            {
                _context.Entry(product.Catagory).State = System.Data.Entity.EntityState.Unchanged;
                _context.Products.Add(product);
                _context.SaveChanges();
            }
        }

        //Updata Product Function
        public void UpdateProduct(Product product)
        {
            using (var _context = new CbContext())
            {
             //_context.Entry(product.Catagory).State = EntityState.Unchanged;
            _context.Entry(product).State = System.Data.Entity.EntityState.Modified;

               
                //_context.Catagories.Add(catagory);
                _context.SaveChanges();
            }
        }

        //Delete Product Function
        public void DeleteProduct(int id)
        {
            using (var _context = new CbContext())
            {

                //_context.Entry(catagory).State = System.Data.Entity.EntityState.Modified;
                var product = _context.Products.Find(id);
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();

           
        }
    }
}
