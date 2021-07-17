using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges);
        Task<Company> GetCompanyAsync(Guid id, bool trackChanges);

        Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids,bool trackChanges);
        void CreateCompany(Company copmany);
        void DeleteCompany(Company company);
    }
}
