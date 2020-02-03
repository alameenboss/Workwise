using System.Collections.Generic;
using Workwise.Data.Models;

namespace Workwise.Service.Interface
{
    public interface ICompanyService
    {
        IEnumerable<CompanyViewModel> GetAllCompanies();
    }
}