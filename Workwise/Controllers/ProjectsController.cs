using System.Web.Mvc;

namespace Workwise.Controllers
{
    [Authorize]
    public class ProjectsController : BaseController
    {
        // GET: Conpanies
        public ActionResult Index()
        {
            return View();
        }
    }
}