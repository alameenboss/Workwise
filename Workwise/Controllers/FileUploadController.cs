using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Workwise.Helper;
using Workwise.Service.Interface;
using Workwise.ViewModel;

namespace Workwise.Controllers
{
    [Authorize]
    public class FileUploadController : BaseController
    {
        private readonly IUserService _userService;

        public FileUploadController(IUserService userService)
        {
            _userService = userService;

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
            _userService.SaveUserImage(User.Identity.GetUserId(), profilePicUrl);

            SessionHelper.Get<UserProfileViewModel>(User.Identity.GetUserId()).ImageUrl = profilePicUrl;
            SessionHelper.UserImage = profilePicUrl;
            return Json(new { success = true, imageUrl = profilePicUrl }, JsonRequestBehavior.AllowGet);
        }
        

    }
}