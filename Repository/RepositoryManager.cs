using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ICompanyRepository _compenyRepository;
        private IEmployeeRepository _employeeRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public ICompanyRepository Company { get
            {
                if (_compenyRepository == null) _compenyRepository = new CompanyRepository(_repositoryContext);
                return _compenyRepository;
            } 
        }

        public IEmployeeRepository Employee
        {
            get
            {
                if (_employeeRepository == null) _employeeRepository = new EmployeeRepository(_repositoryContext);
                return _employeeRepository;
            }
            }

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
        
    }
}
