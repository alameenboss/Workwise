using Workwise.API.Models;

namespace Workwise.API.Data.Interface
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAllCompanies();
    }
}
