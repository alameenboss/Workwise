using Workwise.API.Data.Interface;
using Workwise.API.Models;
using Workwise.API.Service.Interface;

namespace Workwise.API.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return _companyRepository.GetAllCompanies();
        }
    }
}