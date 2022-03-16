using System;

namespace challenge.Models
{
    public class Compensation
    {
        public string CompensationId { get; set; }
        public string EmployeeId { get; set; }
        public double Salary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}