using System.Collections.Generic;
using Workwise.Data.Models;

namespace Workwise.Data.Interface
{
    public interface ICompanyRepository
    {
        IEnumerable<CompanyViewModel> GetAllCompanies();
    }
}
