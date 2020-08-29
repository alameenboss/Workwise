using System.Collections.Generic;
using Workwise.ViewModel;

namespace Workwise.ServiceAgent.Interface
{
    public interface ICompanyServiceAgent
    {
        IEnumerable<CompanyViewModel> GetAllCompanies();
    }
}