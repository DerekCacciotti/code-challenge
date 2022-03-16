using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;

namespace challenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger _logger;

        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository)
        {
            _compensationRepository = compensationRepository;
            _logger = logger;
        }

        public Compensation Create(Compensation compensation)
        {
            if (compensation != null)
            {
                _compensationRepository.Add(compensation);
                _compensationRepository.SaveAsync().Wait();
            }
            return compensation;
        }

        public Compensation GetByEmployeeId(string employeeId)
        {
            if (!string.IsNullOrEmpty(employeeId))
            {
                return _compensationRepository.GetbyEmployeeID(employeeId);
            }
            return null;
        }

        public Compensation GetById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return _compensationRepository.GetById(id);
            }
            return null;
        }

        public Compensation Update(string id, Compensation compensation)
        {
            var compensationToBeDeleted = _compensationRepository.GetById(id);
            if (compensationToBeDeleted != null)
            {
                _compensationRepository.Remmove(compensationToBeDeleted);
                _compensationRepository.SaveAsync().Wait();
                _compensationRepository.Add(compensation);
                compensation.CompensationId = id;
            }
            _compensationRepository.SaveAsync().Wait();
            return compensation;
        }
    }
}