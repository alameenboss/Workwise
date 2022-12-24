using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Workwise.Helper;
using Workwise.ServiceAgent.Interface;
using Workwise.ViewModel;

namespace Workwise.Controllers
{
    [Authorize]
    public class FileUploadController : BaseController
    {
        private readonly IUserServiceAgent _userServiceAgent;

        public FileUploadController(
            IUserServiceAgent userService)
        {
            _userServiceAgent = userService;

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
            var model = new UserImageViewModel
            {
                UserId = User.Identity.GetUserId(),
                ImageUrl = profilePicUrl
            };
            _userServiceAgent.SaveProfileImage(model);
            
            SessionHelper.Get<UserProfileViewModel>(User.Identity.GetUserId()).ImageUrl = profilePicUrl;
            SessionHelper.UserImage = profilePicUrl;
            return Json(new { success = true, imageUrl = profilePicUrl }, JsonRequestBehavior.AllowGet);
        }
        

    }
}