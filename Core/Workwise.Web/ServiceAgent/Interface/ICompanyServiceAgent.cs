using System.Collections.Generic;
using System.Threading.Tasks;
using Workwise.ViewModel;

namespace Workwise.ServiceAgent.Interface
{
    public interface ICompanyServiceAgent
    {
        IEnumerable<CompanyViewModel> GetAllCompanies();
    }
}