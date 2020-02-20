using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Workwise.Data;
using Workwise.Data.Interface;
using Workwise.Model;
using Workwise.Service.Interface;

namespace Workwise.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService()
        {
            _companyRepository = new CompanyRepository();
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return _companyRepository.GetAllCompanies();
        }
    }
}