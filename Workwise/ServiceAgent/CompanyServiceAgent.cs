using System.Collections.Generic;
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

        public IEnumerable<CompanyViewModel> GetAllCompanies()
        {
            return _httpClient.Get<IEnumerable<CompanyViewModel>>(Constent.Company.GetCompanies);
        }
    }
}