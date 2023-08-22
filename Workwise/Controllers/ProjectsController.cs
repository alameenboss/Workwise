using System.Web.Mvc;

namespace Workwise.Controllers
{
    [Authorize]
    public class ProjectsController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}