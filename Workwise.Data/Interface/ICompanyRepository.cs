using System.Collections.Generic;
using Workwise.Model;

namespace Workwise.Data.Interface
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAllCompanies();
    }
}
