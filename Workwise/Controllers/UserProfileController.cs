using System.Web.Mvc;

namespace Workwise.Controllers
{
    [Authorize]
    public class UserProfileController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}