using System.Web.Mvc;

namespace Workwise.Controllers
{
    [Authorize]
    public class UserProfileController : BaseController
    {
        // GET: Conpanies
        public ActionResult Index()
        {
            return View();
        }
    }
}