using System;
using System.Collections.Generic;
using System.Text;

namespace Entites
{
    public class tblEmployees
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeCode { get; set; }
        public double? DateOfJoining { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Salary { get; set; }
    }
}
