using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workwise.Helper;
using Workwise.ServiceAgent.Interface;
using Workwise.ViewModel;
using Workwise.Web.Helper;

namespace Workwise.Web.Controllers
{
    [Authorize]
    public class FileUploadController : Controller
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
            var filePath = Path.GetTempFileName();
            var profilePicUrl = ImageHelper.SaveBaseEnCodedToImage(image, filePath);
            var model = new UserImageViewModel
            {
                UserId = "alameen02111988",
                ImageUrl = profilePicUrl
            };
            _userServiceAgent.SaveProfileImage(model);
            
            SessionHelper.Get<UserProfileViewModel>("alameen02111988").ImageUrl = profilePicUrl;
            SessionHelper.UserImage = profilePicUrl;
            return Ok(new { success = true, imageUrl = profilePicUrl });
        }
        

    }
}