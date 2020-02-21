using System.Web.Mvc;

namespace Workwise.Controllers
{
    [Authorize]
    public class ProfileAccountSettingController : BaseController
    {
        // GET: Conpanies
        public ActionResult Index()
        {
            return View();
        }
    }
}