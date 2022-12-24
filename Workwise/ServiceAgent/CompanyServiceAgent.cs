using System.Collections.Generic;
using System.Threading.Tasks;
using Workwise.ServiceAgent.Interface;
using Workwise.ViewModel;

namespace Workwise.ServiceAgent
{
    public class CompanyServiceAgent : ICompanyServiceAgent
    {
        private readonly IHttpClient _httpClient;
        public CompanyServiceAgent(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public  IEnumerable<CompanyViewModel> GetAllCompanies()
        {
            return _httpClient.GetAsync<IEnumerable<CompanyViewModel>>(Constent.Company.GetCompanies);
        }
    }
}