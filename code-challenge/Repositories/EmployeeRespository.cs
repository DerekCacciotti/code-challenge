using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using challenge.Data;

namespace challenge.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        public Employee GetById(string id)
        {
            // added include statement to bring in child entities
            return _employeeContext.Employees.Include("DirectReports").Select(e => new Employee
            {
                EmployeeId = e.EmployeeId,
                Department = e.Department,
                FirstName = e.FirstName,
                DirectReports = e.DirectReports.Select(x => new Employee
                {
                    EmployeeId = x.EmployeeId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Department = x.Department,
                    Position = x.Position,
                    DirectReports = x.DirectReports.Select(se => new Employee
                    {
                        Position = se.Position,
                        EmployeeId = se.EmployeeId,
                        FirstName = se.FirstName,
                        LastName = se.LastName,
                        Department = se.Department,
                        DirectReports = se.DirectReports
                    }).ToList(),
                }).ToList(),
                LastName = e.LastName,
                Position = e.Position,
            }).SingleOrDefault(x => x.EmployeeId == id);
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }
    }
}