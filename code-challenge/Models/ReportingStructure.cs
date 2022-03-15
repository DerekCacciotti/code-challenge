using System.Linq;

namespace challenge.Models
{
    public class ReportingStructure
    {
        public Employee Employee { get; set; }
        public int numberOfReports { get; set; }

        public ReportingStructure(Employee employee)
        {
            Employee = employee;
            // get the count of top level employee entities
            var EmployeeCount = employee.DirectReports.Count();
            // get the count of reports for any child entities.
            var SubEmployeeCount = employee.DirectReports.SelectMany(x => x.DirectReports).ToList().Count();
            // calculate the sum.
            numberOfReports = EmployeeCount + SubEmployeeCount;
        }
    }
}