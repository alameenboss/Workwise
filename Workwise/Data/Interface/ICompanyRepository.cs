using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workwise.Models;

namespace Workwise.Data.Interface
{
    interface ICompanyRepository
    {
        IEnumerable<CompanyViewModel> GetAllCompanies();
    }
}
