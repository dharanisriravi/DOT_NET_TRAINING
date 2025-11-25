using EmployeeRecord;
using System;

namespace EmployeeRecord
{
    public interface IEmployeeDataReader
    {
        Employee_record GetEmployeeRecords(int employeeid);
    }

    public class MockEmployeeDataReader : IEmployeeDataReader
    {
        public Employee_record GetEmployeeRecords(int employeeid)
        {
            if (employeeid == 102)
            {
                return new Employee_record
                {
                    Id = 102,
                    Name = "Dharanisri",
                    Role = "Manager",
                    IsVeteran = true
                };
            }
            if (employeeid == 101)
            {
                return new Employee_record
                {
                    Id = 101,
                    Name = "Surya",
                    Role = "Developer",
                    IsVeteran = false
                };
            }
            if (employeeid == 103)
            {
                return new Employee_record
                {
                    Id = 103,
                    Name = "Harinisri",
                    Role = "Intern",
                    IsVeteran = false
                };
            }
            return new Employee_record
            {
                Id = employeeid,
                Name = "Unknown",
                Role = "Unknown",
                IsVeteran = false
            };
        }
    }
}
