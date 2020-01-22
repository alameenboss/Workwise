using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workwise.Models;
using Microsoft.AspNet.Identity;
using Workwise.Data;

namespace Workwise.Controllers
{
    [Authorize]
    public class MyProfileFeedController : Controller
    {
        //private readonly ApplicationDbContext _context;
        //public MyProfileFeedController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        // GET: Conpanies
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginPartial()
        {
            UserProfileRepository repo = new UserProfileRepository();
            var model = repo.GetByUserId(User.Identity.GetUserId());
            if (!(model?.Id > 0))
            {
                model = new UserProfile()
                {
                    FirstName = User.Identity.GetUserName(),
                    ImageUrl = @"/images/alameen_user.jpg"
                };
            }
            return PartialView("_LoginPartial", model);
        }


    }
}