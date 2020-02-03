using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Workwise.Data.Interface;
using Workwise.Helper;
using Workwise.Data.Models;

namespace Workwise.Controllers
{
    [Authorize]
    public class FileUploadController : BaseController
    {
        private readonly IUserProfileRepository _userProfileRepo;

        public FileUploadController(IUserProfileRepository userProfileRepo)
        {
            _userProfileRepo = userProfileRepo;

        }
        // GET: FileUpload
        public ActionResult Index()
        {
            return View();
        }

 
        [HttpPost]
        public ActionResult SaveImage(string image)
        {
            var profilePicUrl = ImageHelper.SaveBaseEnCodedToImage(image, Server.MapPath("~/Images/Upload"));
            _userProfileRepo.SaveUserImage(User.Identity.GetUserId(), profilePicUrl);
            SessionHelper.Get<UserProfile>(User.Identity.GetUserId()).ImageUrl = profilePicUrl;
            SessionHelper.UserImage = profilePicUrl;
            return Json(new { success = true, imageUrl = profilePicUrl }, JsonRequestBehavior.AllowGet);
        }
        

    }
}