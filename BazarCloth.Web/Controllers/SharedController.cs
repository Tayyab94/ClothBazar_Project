using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BazarCloth.Web.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        public JsonResult UploadImage()
        {
            JsonResult result = new JsonResult();

            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            try
            {

                var file = Request.Files[0];


                //var fileName = file.FileName;

                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                var path = Path.Combine(Server.MapPath("~/Content/Uploads/"),fileName);

                file.SaveAs(path);

                result.Data = new { Success = true, ImageURL = string.Format("/Content/Uploads/{0}",fileName) };

            }
            catch (Exception e)
            {
                result.Data = new { Success = false, Message = e.Message };
            }

            return result;
        }
    }
}