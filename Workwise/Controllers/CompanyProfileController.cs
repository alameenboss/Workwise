using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Workwise.Controllers
{
    [Authorize]
    public class CompanyProfileController : Controller
    {
        // GET: Conpanies
        public ActionResult Index()
        {
            return View();
        }
    }
}