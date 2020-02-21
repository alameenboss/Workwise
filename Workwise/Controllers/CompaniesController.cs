using System.Linq;
using System.Web.Mvc;
using Workwise.ServiceAgent.Interface;

namespace Workwise.Controllers
{
    [Authorize]
    public class CompaniesController : BaseController
    {
        private readonly ICompanyServiceAgent _companyServiceAgent;

      
        public CompaniesController(ICompanyServiceAgent companyServiceAgent)
        {
            _companyServiceAgent = companyServiceAgent;
        }


        public ActionResult Index()
        {

            var model = _companyServiceAgent.GetAllCompanies().ToList();

            return View(model);
        }
    }
}