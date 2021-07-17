using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CompanyEmployees.ActionFilters;
using Contracts;
using Entites.DataTransferObjects;
using Entites.RequestFeatures;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CompanyEmployees.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EmployeesController(IRepositoryManager repositoryManager,ILoggerManager logger,IMapper mapper)
        {
            _repository = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }           

        [HttpGet]        
        public async Task<IActionResult> GetEmployeesForCompany(Guid companyId,[FromQuery] EmployeeParameters employeeParameter)
        {
            if(!employeeParameter.ValidRange)
            {
                return BadRequest("Max age can't be less than min age");
            }
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges: false);
            if(company == null)
            {
                _logger.LogInfo($"Company with id {companyId} dosen't exist in the database");
                return NotFound();
            }
            var employeesFromDb = _repository.Employee.GetEmployees(companyId,employeeParameter, trackChanges: false);

            Response.Headers.Add("X-Pagination",
                    JsonConvert.SerializeObject(employeesFromDb.MetaData)); 

            var employeeDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);
            return Ok(employeeDto);
        }

        [HttpGet("{id}",Name = "GetEmployeeForCompany")]
        public async Task<IActionResult> GetEmployeeForCompany(Guid companyId , Guid id)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges: false);
            if(company == null)
            {
                _logger.LogInfo($"Company with id {companyId} doesn't exist in the database");
                return NotFound();
            }
            var employeeFromDb = _repository.Employee.GetEmployee(companyId, id, trackChanges: false);
            if(employeeFromDb == null)
            {
                _logger.LogInfo($"Employee with id {id} doesn't exist in the database");
                return NotFound();
            }
            var employee = _mapper.Map<EmployeeDto>(employeeFromDb);
            return Ok(employee);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateEmployeeForCompany(Guid companyId ,[FromBody]EmployeeForCreationDto employee)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, false);
            if(company == null)
            {
                _logger.LogInfo($"Company with id {companyId} dosesn't exist in the database");
                return NotFound();
            }

            var employeeEnity = _mapper.Map<Employee>(employee);

            _repository.Employee.CreateEmployeeForCompany(companyId, employeeEnity);
            await _repository.SaveAsync();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEnity);
            return CreatedAtAction("GetEmployeeForCompany", new { companyId ,id = employeeToReturn.Id },employeeToReturn);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId,Guid id )
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, false);
            if(company == null)
            {
                _logger.LogInfo($"Company with id {companyId} doesn't exist in the database");
                return NotFound();
            }
            var employee = _repository.Employee.GetEmployee(companyId, id, false);
            if(employee == null)
            {
                _logger.LogInfo($"Employee with id {id} doesn't exist in the database");
                return NotFound();
            }
            _repository.Employee.DeleteEmployee(employee);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId,Guid id ,EmployeeForUpdateDto employee)
        {
            if(employee == null)
            {
                _logger.LogError("EmployeeForUpdateDto object sent from client is null");
                return BadRequest("EmployeeForUpdateDto object is null");
            }
            var company = await _repository.Company.GetCompanyAsync(companyId,trackChanges:false);
            if(company == null)
            {
                _logger.LogInfo($"Company with id {companyId} doesn't exist in the database");
                return NotFound();
            }
            var employeeEntity = _repository.Employee.GetEmployee(companyId, id, trackChanges: true);
            if(employeeEntity == null )
            {
                _logger.LogInfo($"Employee with id : {id} doesnt exist in the database");
                return NotFound();
            }
            _mapper.Map(employee, employeeEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(Guid companyId,Guid id,
            [FromBody]JsonPatchDocument<EmployeeForUpdateDto> pathDoc)
        {
            if(pathDoc == null)
            {
                _logger.LogError("pathDoc object sent from client is null");
                return BadRequest("PathDoc object is null");
            }
            var company = await _repository.Company.GetCompanyAsync(companyId, false);
            if(company == null)
            {
                _logger.LogInfo($"Company with id {companyId} doesn't exist in the database");
                return NotFound();
            }
            var employeeEntity = _repository.Employee.GetEmployee(companyId, id, trackChanges: true);
            if(employeeEntity == null)
            {
                _logger.LogInfo($"Employee with id {id} doesn't exist in the database");
                return NotFound();
            }
            var employeeToPath = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);
           
            pathDoc.ApplyTo(employeeToPath);
            
            _mapper.Map(employeeToPath, employeeEntity);

            await _repository.SaveAsync();

            return NoContent();

        }
    }

}
