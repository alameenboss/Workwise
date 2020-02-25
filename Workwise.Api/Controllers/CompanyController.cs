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
        // GET api/values
        public IEnumerable<Company> GetAllCompanies()
        {
            return _companyService.GetAllCompanies();
        }

        //// GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}
