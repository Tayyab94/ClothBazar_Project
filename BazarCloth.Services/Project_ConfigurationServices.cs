using BazarCloth.DataBase;
using BazarCloth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazarCloth.Services
{
    public  class Project_ConfigurationServices
    {


        #region Apply_SingleTon

        public static Project_ConfigurationServices Instance
        {
            get
            {
                if (instance == null)
                    instance = new Project_ConfigurationServices();

                return instance;
            }
        }


        private static Project_ConfigurationServices instance { get; set; }


        private Project_ConfigurationServices()
        {

        }

        #endregion
        public BazarClothConfig GetConfig(string key)
        {
            using (CbContext _context = new CbContext())
            {
                return _context.bazarClothConfigs.FirstOrDefault(x => x.SKey == key);
            }
        }


        //This function returns the pageSize, which we have configure into
        public int GetPageSize()
        {
            using (CbContext _context = new CbContext())
            {
                var PageSizeConfig = _context.bazarClothConfigs.Find("pageSize");
                return PageSizeConfig != null ? int.Parse(PageSizeConfig.SValue) : 5;
            }
        }

        //This function returns the pageSize, which we have configure into
        public int GetPageSizeForShopPage()
        {
            using (CbContext _context = new CbContext())
            {
                var PageSizeConfig = _context.bazarClothConfigs.Find("pageSizeForShop");
                return PageSizeConfig != null ? int.Parse(PageSizeConfig.SValue) : 6;
            }
        }
    }
}
