using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workwise.ServiceAgent.Interface;

namespace Workwise.Web.Controllers
{
    [Authorize]
    public class CompaniesController : Controller
    {
        private readonly ICompanyServiceAgent _companyServiceAgent;

      
        public CompaniesController(ICompanyServiceAgent companyServiceAgent)
        {
            _companyServiceAgent = companyServiceAgent;
        }


        public ActionResult Index()
        {
            var model =_companyServiceAgent.GetAllCompanies().ToList();
            return View(model);
        }
    }
}