using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Workwise.Controllers
{
    public class IndexController : Controller
    {
        // GET: Conpanies
        public ActionResult Index()
        {
            return View();
        }
    }
}