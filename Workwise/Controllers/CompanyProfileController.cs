using System.Web.Mvc;

namespace Workwise.Controllers
{
    [Authorize]
    public class CompanyProfileController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}