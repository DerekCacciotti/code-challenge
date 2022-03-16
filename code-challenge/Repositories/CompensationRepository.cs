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
    public class CompensationRepository : ICompensationRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, EmployeeContext context)
        {
            _employeeContext = context;
            _logger = logger;
        }

        public Compensation Add(Compensation compensation)
        {
            compensation.CompensationId = Guid.NewGuid().ToString();
            _employeeContext.Add(compensation);
            return compensation;
        }

        public Compensation GetbyEmployeeID(string employeeID)
        {
            return _employeeContext.Compensations.FirstOrDefault(x => x.EmployeeId == employeeID);
        }

        public Compensation GetById(string id)
        {
            return _employeeContext.Compensations.FirstOrDefault(x => x.CompensationId == id);
        }

        public Compensation Remmove(Compensation compensation)
        {
            return _employeeContext.Remove(compensation).Entity;
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }
    }
}