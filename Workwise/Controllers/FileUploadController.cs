using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workwise.Data;
using Workwise.Models;

namespace Workwise.Controllers
{
    [Authorize]
    public class FileUploadController : Controller
    {
        // GET: FileUpload
        public ActionResult Index()
        {
            return View();
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static Image Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            //return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            Image image;
            using (MemoryStream ms = new MemoryStream(base64EncodedBytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }

        [HttpPost]
        public ActionResult SaveImage(string image)
        {
            
            var fileName = Guid.NewGuid().ToString().Replace("-", "").Replace(" ", "") + ".png";
            var path = Path.Combine(Server.MapPath("~/Images/Upload"), fileName);
            var imgUrl = @"/Images/Upload/" + fileName;
            image = image.Split(';')[1].Split(',')[1];
            var obj = Base64Decode(image);
            obj.Save(path, System.Drawing.Imaging.ImageFormat.Png);

            new UserProfileRepository().SaveUserImage(User.Identity.GetUserId(), imgUrl);
            
            return Json(new { success = true, imageUrl = imgUrl }, JsonRequestBehavior.AllowGet);
        }
    }
}