using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;

namespace Workwise.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private ApplicationUserManager _userManager;


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }
    }
}