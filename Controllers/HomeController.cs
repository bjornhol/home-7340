using Home7340.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Home7340.Controllers
{    
    public class HomeController : Controller
    {
        [BasicAuthentication]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadSnapshot()
        {
            HttpRequestWrapper request = Request as HttpRequestWrapper;
            
            string name = request.Form["name"];

            if (request != null && request.Files.Count > 0)
            {
                HttpPostedFileBase file = request.Files[0];

                var images = HostingEnvironment.MapPath("~/Content/Uploads/Images");
                Directory.CreateDirectory(images);
                string fileName = Path.Combine(images, name + (new FileInfo(file.FileName)).Extension);
                
                var info = new FileInfo(fileName);
                if (info.Exists)
                {
                    info.Delete();
                }

                file.SaveAs(fileName);
            }

            return Content("done");
        }
    }
}