using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workwise.Models;
using Workwise.Service;
using Workwise.Service.Interface;

namespace Workwise.Controllers
{
    [Authorize]
    public class CompaniesController : Controller
    {
        private readonly ICompanyService _companyService;

      
        public CompaniesController()
        {
            _companyService = new CompanyService();
        }


        public ActionResult Index()
        {

            var model = _companyService.GetAllCompanies().ToList();

            return View(model);
        }
    }
}