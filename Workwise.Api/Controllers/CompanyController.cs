using System;
using System.Collections.Generic;
using System.Web.Http;
using Workwise.Model;
using Workwise.Service.Interface;

namespace Workwise.Api.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService = null;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
       

        public IEnumerable<Company> GetAllCompanies()
        {
            return _companyService.GetAllCompanies();
        }
    }
}
