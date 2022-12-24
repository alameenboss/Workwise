using Microsoft.AspNetCore.Mvc;
using Workwise.API.Models;
using Workwise.API.Service.Interface;

namespace Workwise.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService companyService;

        public CompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        [HttpGet("GetAllCompanies")]
        public IActionResult GetAllCompanies()
        {
            IEnumerable<Company> result = companyService.GetAllCompanies();
            return result == null ? NotFound() : Ok(result);
        }
    }
}
