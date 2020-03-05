using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
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

        [HttpGet]
        [ResponseType(typeof(IEnumerable<Company>))]
        public IHttpActionResult GetAllCompanies()
        {
            try
            {
                var result = _companyService.GetAllCompanies();
                if (result == null)
                {
                    var resp = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Not Found"),
                        ReasonPhrase = "Not Found"
                    };
                    throw new HttpResponseException(resp);
                }
                return Ok(result);
            }
            catch (HttpResponseException)
            {

                throw;
            }
        }
    }
}
