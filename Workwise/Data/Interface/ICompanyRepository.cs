using System.Collections.Generic;
using Workwise.Models;

namespace Workwise.Data.Interface
{
    public interface ICompanyRepository
    {
        IEnumerable<CompanyViewModel> GetAllCompanies();
    }
}
