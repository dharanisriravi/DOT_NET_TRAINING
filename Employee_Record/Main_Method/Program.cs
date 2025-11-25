
using EmployeeRecord;
using System;

namespace Main_demo
{
    public class Program
    {
        public static void Main()
        {
            IEmployeeDataReader reader = new MockEmployeeDataReader();
            PayrollProcessor processor = new PayrollProcessor(reader);

            Console.WriteLine("Total Compensation for 101: " + processor.CalculateTotalCompensation(101));
            Console.WriteLine("Total Compensation for 102: " + processor.CalculateTotalCompensation(102));
            Console.WriteLine("Total Compensation for 103: " + processor.CalculateTotalCompensation(103));
            Console.ReadLine();
        }
    }
}
