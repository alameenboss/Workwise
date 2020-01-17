using System.Collections.Generic;
using Workwise.Models;

namespace Workwise.Service.Interface
{
    public interface ICompanyService
    {
        IEnumerable<CompanyViewModel> GetAllCompanies();
    }
}