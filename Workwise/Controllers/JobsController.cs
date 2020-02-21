using System.Web.Mvc;

namespace Workwise.Controllers
{
    [Authorize]
    public class JobsController : BaseController
    {
        // GET: Conpanies
        public ActionResult Index()
        {
            return View();
        }
    }
}