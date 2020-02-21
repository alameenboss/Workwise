using System.Collections.Generic;
using Workwise.ServiceAgent.Interface;
using Workwise.ViewModel;

namespace Workwise.ServiceAgent
{
    public class CompanyServiceAgent : ICompanyServiceAgent
    {

        public IEnumerable<CompanyViewModel> GetAllCompanies()
        {
            return new List<CompanyViewModel>();
        }
    }
}