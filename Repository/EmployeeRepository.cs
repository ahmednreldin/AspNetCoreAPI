using Contracts;
using Entites.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        { }

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }

        public Employee GetEmployee(Guid companyId, Guid id, bool trackChanges)
        => FindByCondition(e => e.CompanyId.Equals(companyId) 
                            && e.Id.Equals(id),trackChanges)
                                         .SingleOrDefault();

        public PagedList<Employee> GetEmployees(Guid companyId,EmployeeParameters employeeParameters ,bool trackChanges)
        {
            var employee = FindByCondition(e => e.CompanyId.Equals(companyId) 
            && e.Age <= employeeParameters.MaxAge && e.Age >= employeeParameters.MinAge
             , trackChanges).OrderBy(e => e.Name).ToList();

            return PagedList<Employee>.ToPagedList(employee, employeeParameters.PageNumber, employeeParameters.PageSize);
        }
           
    }
}
