using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workwise.Data;

namespace Workwise.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        // GET: Conpanies
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUsers()
        {
            var userprofilerepo = new UserProfileRepository();
            var model = userprofilerepo.GetAllUsers().Where(x => x.UserId != User.Identity.GetUserId()).ToList();
            return PartialView(@"~\Views\Messages\_UserPartial.cshtml", model);
        }
    }
}