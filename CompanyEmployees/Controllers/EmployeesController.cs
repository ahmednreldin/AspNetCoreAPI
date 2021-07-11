﻿using System;
using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Entites.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetEmployeesForCompany(Guid companyId)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges: false);
            if(company == null)
            {
                _logger.LogInfo($"Company with id {companyId} dosen't exist in the database");
                return NotFound();
            }
            var employeesFromDb = _repository.Employee.GetEmployees(companyId, trackChanges: false);
            var employeeDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);
            return Ok(employeeDto);
        }
        [HttpGet("{id}")]
        public IActionResult GetEmployeeForCompany(Guid companyId , Guid id)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges: false);
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
    }

}
