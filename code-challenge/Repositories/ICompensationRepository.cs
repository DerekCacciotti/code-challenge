using challenge.Models;
using System;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    public interface ICompensationRepository
    {
        Compensation GetById(string id);

        Compensation GetbyEmployeeID(string employeeID);

        Compensation Add(Compensation compensation);

        Compensation Remmove(Compensation compensation);

        Task SaveAsync();
    }
}