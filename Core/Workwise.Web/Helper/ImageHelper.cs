using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net;
using System.Web;

namespace Workwise.Helper
{
    public static  class ImageHelper
    {
        public static string SaveBaseEnCodedToImage(string base64String,string serverPath)
        {
            var fileName = Guid.NewGuid().ToString().Replace("-", "").Replace(" ", "") + ".png";
            var path = Path.Combine(serverPath, fileName);
            var imgUrl = @"/Images/Upload/" + fileName;
            base64String = base64String.Split(';')[1].Split(',')[1];
            var profilePic = Base64Helper.Base64Decode(base64String);
            profilePic.Save(path, System.Drawing.Imaging.ImageFormat.Png);
            return imgUrl;
        }

        public static string SavePostedFile(IFormFile postedFile, string serverPath)
        { 
            var extention = Path.GetExtension(postedFile.FileName);
            var fileName = Guid.NewGuid().ToString().Replace("-", "").Replace(" ", "") + "." + extention;
            var path = Path.Combine(serverPath, fileName);
            var imgUrl = @"/Images/Upload/" + fileName;
            //postedFile.SaveAs(path);
            using (var stream = System.IO.File.Create(path))
            {
                postedFile.CopyToAsync(stream);
            }
            return imgUrl;
        }

        public static string SaveImagefromWeb(string url, string serverPath)
        {
            var fileName = Guid.NewGuid().ToString().Replace("-", "").Replace(" ", "") + ".png";

            var path = Path.Combine(serverPath, fileName);

            using (WebClient client = new WebClient())
            {
                client.DownloadFileAsync(new Uri(url), path);
            }

            return @"/Images/Upload/" + fileName;
        }

    }
}