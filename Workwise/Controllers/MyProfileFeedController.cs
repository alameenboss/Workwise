using System.Web.Mvc;
using Workwise.Helper;

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


        public ActionResult Index(string mode)
        {
            ViewData["Mode"] = mode;
            ViewData["ImageUrl"] = SessionHelper.GetUser(User).ImageUrl;
            return View();
        }

        public ActionResult LoginPartial()
        {
            var model = SessionHelper.GetUser(User);
            return PartialView("_LoginPartial", model);
        }



        
    }
}