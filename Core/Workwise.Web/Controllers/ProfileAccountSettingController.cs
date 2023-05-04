using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workwise.Helper;
using Workwise.ServiceAgent.Interface;
using Workwise.ViewModel;

namespace Workwise.Web.Controllers
{
    [Authorize]
    public class ProfileAccountSettingController : Controller
    {
        // GET: Conpanies
        public ActionResult Index()
        {
            return View();
        }
    }
}