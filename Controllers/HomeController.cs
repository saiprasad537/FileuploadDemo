using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileuploadDemo.Controllers
{
    public class HomeController : Controller
    {

         public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile()
        {
            try
            {
                //Demo hayageek code files
                //This is made for Production Use Kindly test the changes
                HttpPostedFileBase hpf = HttpContext.Request.Files["file"] as HttpPostedFileBase;
                string tag = HttpContext.Request.Params["tags"];// The same param name that you put in extrahtml if you have some.
                string category = HttpContext.Request.Params["category"];

                DirectoryInfo di = Directory.CreateDirectory(Server.MapPath("~/Tmp/Files"));// If you don't have the folder yet, you need to create.
                string sentFileName = Path.GetFileName(hpf.FileName); //it can be just a file name or a user local path! it depends on the used browser. So we need to ensure that this var will contain just the file name.
                string savedFileName = Path.Combine(di.FullName, sentFileName);
                hpf.SaveAs(savedFileName);
                        
                var msg = new { msg = "File Uploaded", filename = hpf.FileName, url= savedFileName };
                //passing Json format for Fileupload PlugIn
                return Json(msg,"text/x-json");
            }
            catch (Exception e)
            {
                //If you want this working with a custom error you need to change in file jquery.uploadfile.js, the name of 
                //variable customErrorKeyStr in line 85, from jquery-upload-file-error to jquery_upload_file_error 
                var msg = new { jquery_upload_file_error = e.Message };
                return Json(msg, "text/x-json");
            }
        }
      
        public ActionResult DownloadFile(string url)
        {
            return File(url,"");
        }

        [HttpPost]
        public ActionResult DeleteFile(string url)
        {
            try
            {
                System.IO.File.Delete(url);
                var msg = new { msg = "File Deleted!" };
                return Json(msg);
            }
            catch (Exception e)
            {
                //If you want this working with a custom error you need to change the name of 
                //variable customErrorKeyStr in line 85, from jquery-upload-file-error to jquery_upload_file_error 
                var msg = new { jquery_upload_file_error = e.Message };
                return Json(msg);
            }
        }
        //
        // GET: /Home/

       
    }
}
