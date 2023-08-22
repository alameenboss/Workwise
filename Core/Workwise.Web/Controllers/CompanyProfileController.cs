using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Workwise.Web.Controllers
{
    [Authorize]
    public class CompanyProfileController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}