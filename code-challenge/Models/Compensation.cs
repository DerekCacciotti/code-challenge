using System;

namespace challenge.Models
{
    public class Compensation
    {
        public string CompensationId { get; set; }
        public string EmployeeId { get; set; }
        public double salary { get; set; }
        public DateTime effectiveDate { get; set; }
    }
}