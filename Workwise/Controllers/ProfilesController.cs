using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workwise.Data;

namespace Workwise.Controllers
{
    [Authorize]
    public class ProfilesController : Controller
    {
        // GET: Conpanies
        public ActionResult Index()
        {

            var userRepo = new UserProfileRepository();
            var model = userRepo.GetAllUsers();
            return View(model);
        }
    }
}