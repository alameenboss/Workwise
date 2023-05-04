//using Microsoft.AspNetCore.Authorization;

//namespace Workwise.Web.Controllers
//{
//    [Authorize]
//    public class BaseController : Controller
//    {
//        private ApplicationUserManager _userManager;


//        public ApplicationUserManager UserManager
//        {
//            get
//            {
//                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
//            }
//            set
//            {
//                _userManager = value;
//            }
//        }
//    }
//}