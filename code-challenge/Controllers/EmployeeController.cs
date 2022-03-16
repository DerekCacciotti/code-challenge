using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;

namespace challenge.Controllers
{
    [Route("api/employee")]
    public class EmployeeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;
        private readonly ICompensationService _compensationService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService, ICompensationService compensationService)
        {
            _logger = logger;
            _employeeService = employeeService;
            _compensationService = compensationService;
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            _logger.LogDebug($"Received employee create request for '{employee.FirstName} {employee.LastName}'");

            _employeeService.Create(employee);

            return CreatedAtRoute("getEmployeeById", new { id = employee.EmployeeId }, employee);
        }

        [HttpGet("{id}", Name = "getEmployeeById")]
        public IActionResult GetEmployeeById(String id)
        {
            _logger.LogDebug($"Received employee get request for '{id}'");

            var employee = _employeeService.GetById(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPut("{id}")]
        public IActionResult ReplaceEmployee(String id, [FromBody] Employee newEmployee)
        {
            _logger.LogDebug($"Recieved employee update request for '{id}'");

            var existingEmployee = _employeeService.GetById(id);
            if (existingEmployee == null)
                return NotFound();

            _employeeService.Replace(existingEmployee, newEmployee);

            return Ok(newEmployee);
        }

        /// <summary>
        /// HTTP Get endpoint to get the reporting structure for the passed in employee id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("ReportingStructure/{id}")]
        public IActionResult GetReportingStructure(string id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            var reportingStructure = new ReportingStructure(employee);
            return Ok(reportingStructure);
        }

        [HttpPost("CreateCompensation")]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            if (compensation == null)
            {
                return BadRequest();
            }
            _compensationService.Create(compensation);
            return Ok(compensation);
        }

        [HttpGet("GetCompensation/{id}")]
        public IActionResult GetCompensation(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("EmployeeId is required");
            }

            var compensation = _compensationService.GetByEmployeeId(id);
            if (compensation == null)
            {
                return NotFound();
            }

            return Ok(compensation);
        }
    }
}