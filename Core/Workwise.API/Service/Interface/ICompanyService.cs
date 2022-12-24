using Workwise.API.Models;

namespace Workwise.API.Service.Interface
{
    public interface ICompanyService
    {
        IEnumerable<Company> GetAllCompanies();
    }
}