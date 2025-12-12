using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_TEST_DEC12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Connected connect=new Connected();
             connect.DisplayCourse_Task2_1();
            connect.AddNewStudent_Task2_2();

            Console.WriteLine("Enter the department to search for students:");
             string department = Console.ReadLine();
             connect.SearchStudent_Task2_3(department);

              connect.DisplayCourses_Task2_4();
            connect.UpdategradeTask2_5();

            connect.GetCourseBySem_procedure();

           Disconnected dis = new Disconnected();
            dis.LoadDataAndCourses_Task3_1();

            dis.ModifyCourseTask3_2();

            dis.InsertCourseTask3_3();

            dis.DeleteStudentTask3_4();
        }
    }
}
