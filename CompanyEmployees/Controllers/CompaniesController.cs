using AutoMapper;
using Entites.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;

namespace CompanyEmployees.Controllers
{
    [Route("api/Companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly RepositoryManager _repository;
        private readonly IMapper _mapper;

        public CompaniesController(ILoggerManager logger, RepositoryManager repositroy, IMapper mapper)
        {
            _logger = logger;
            _repository = repositroy;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCompanies()
        {

            var companies = _repository.Company.GetAllCompanies(trackChanges: false);
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return Ok(companiesDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _repository.Company.GetCompany(id, trackChanges: false);
            
            if(company == null)
            {
                _logger.LogInfo($"Company with id {id} dosn't exist in the database");
                return NotFound();
            }
            else
            {
                var companyDto = _mapper.Map<CompanyDto>(company);
                return Ok(companyDto);
            }
        }

    }
}
