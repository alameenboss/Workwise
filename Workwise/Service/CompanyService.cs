using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Workwise.Data;
using Workwise.Data.Interface;
using Workwise.Data.Models;
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

        public IEnumerable<CompanyViewModel> GetAllCompanies()
        {
            return _companyRepository.GetAllCompanies();
        }
    }
}