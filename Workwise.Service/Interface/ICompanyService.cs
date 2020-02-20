using System.Collections.Generic;
using Workwise.Model;

namespace Workwise.Service.Interface
{
    public interface ICompanyService
    {
        IEnumerable<Company> GetAllCompanies();
    }
}