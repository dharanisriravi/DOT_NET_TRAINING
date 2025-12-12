using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_TEST_DEC12
{
    internal class Connected
    {

        SqlConnection con = new SqlConnection("Integrated security = true;database = ADO_Test_dec12;server = (localdb)\\MSSQLLocalDB");


        //Task 2.1 – Display all courses
        // Show: CourseId, CourseName, Credits, Semester
        public void DisplayCourse_Task2_1()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Courses", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Console.WriteLine($"{dr["CourseId"]}   {dr["CourseName"]}   {dr["Credits"]}    {dr["Semester"]} ");
            }
            con.Close();
        }

        //Task 2.2 – Add a new student

        public void AddNewStudent_Task2_2()
        {
            con.Open();

            Console.Write("Enter Student Name: ");
            string FullName = Console.ReadLine();

            Console.Write("Enter Email ID: ");
            string Email = Console.ReadLine();

            Console.Write("Enter Department Name: ");
            string Department = Console.ReadLine();

            Console.Write("Enter Year of Study: ");
            int YearOfStudy = Convert.ToInt32(Console.ReadLine());

            SqlCommand cmd = new SqlCommand("insert into students(fullname, email, department, yearofstudy) values(@FullName, @Email, @Department, @YearOfStudy)", con);

            cmd.Parameters.AddWithValue("@FullName", FullName);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Department", Department);
            cmd.Parameters.AddWithValue("@YearOfStudy", YearOfStudy);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine(rows + " record inserted");

            con.Close();
        }


        //Task 2.3 – Search students by department
        //Input example: “Computer Science”
        ///Display matching students.

        public void SearchStudent_Task2_3(string Department)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("select StudentId , FullName ,Email from Students where Department = @Department", con);
            cmd.Parameters.AddWithValue("@Department", Department);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Console.WriteLine($"{dr["StudentId"]}   {dr["FullName"]}   {dr["Email"]} ");
            }
            con.Close();
        }

        //Task 2.4 – Display enrolled courses for a student

        public void DisplayCourses_Task2_4()
        {
            con.Open();

            Console.WriteLine("Enter Student ID:");
            int studentId = Convert.ToInt32(Console.ReadLine());

            SqlCommand cmd = new SqlCommand(
                "select c.coursename, c.credits, e.enrolldate, e.grade " +
                "from enrollments e " +
                "join courses c on e.courseid = c.courseid " +
                "where e.studentid = @studentId", con);

            cmd.Parameters.AddWithValue("@studentId", studentId);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Console.WriteLine($"{dr["coursename"]}   {dr["credits"]}   {dr["enrolldate"]}   {dr["grade"]}");
            }

            con.Close();
        }


        //Task 2.5 – Update grade(Connected Mode)

        public void UpdategradeTask2_5()
        {
            con.Open();

            Console.WriteLine("Enter Enrollment ID:");
            int enrollmentId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Grade (A/B/C/D/F):");
            string grade = Console.ReadLine();

            SqlCommand cmd = new SqlCommand("update enrollments set grade=@grade where enrollmentid=@enrollmentId", con);
            cmd.Parameters.AddWithValue("@grade", grade);
            cmd.Parameters.AddWithValue("@enrollmentId", enrollmentId);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine($"{rows} record updated successfully.");

            con.Close();
        }



        //procedure


        public void GetCourseBySem_procedure()
        {
            Console.WriteLine("Enter Semester: ");
            string sem = Console.ReadLine();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_GetCoursesBySemester", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p1 = new SqlParameter("@Semester", sem);
            cmd.Parameters.Add(p1);

            SqlDataReader dr = cmd.ExecuteReader();
            Console.WriteLine("Course details by semester: ");

            // Iterate through the data reader and display course details
            while (dr.Read())
            {
                Console.WriteLine($"{dr[0]}\t{dr[1]}\t{dr[2]}");
            }

            dr.Close();
            con.Close();
        }










    }
}
