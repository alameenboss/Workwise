using System.Web.Mvc;

namespace Workwise.Controllers
{
    [Authorize]
    public class ProfileAccountSettingController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}