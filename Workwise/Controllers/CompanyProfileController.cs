﻿using System.Web.Mvc;

namespace Workwise.Controllers
{
    [Authorize]
    public class CompanyProfileController : BaseController
    {
        // GET: Conpanies
        public ActionResult Index()
        {
            return View();
        }
    }
}