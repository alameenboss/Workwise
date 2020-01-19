using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Workwise.Controllers
{
    [Authorize]
    public class IndexController : Controller
    {
        // GET: Conpanies
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPosts()
        {
            var model = DateTime.Now.Second % 2 == 0 ? true : false;
            return PartialView("_PostDetail", model);
        }

    }
}