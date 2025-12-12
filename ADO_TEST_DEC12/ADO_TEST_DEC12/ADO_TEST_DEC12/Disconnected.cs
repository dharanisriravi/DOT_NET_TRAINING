using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_TEST_DEC12
{
    internal class Disconnected
    {



        //Task 3.1 – Load Students and Courses into a DataSet

        public void LoadDataAndCourses_Task3_1()
        {
            SqlConnection con = new SqlConnection("integrated security=true; database=ado_test_dec12; server=(localdb)\\mssqllocaldb");
            SqlDataAdapter da_stud;
            SqlDataAdapter da_course;
            DataSet ds = new DataSet();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            da_stud = new SqlDataAdapter("select * from students", con);
            SqlCommandBuilder sb = new SqlCommandBuilder(da_stud);
            da_stud.Fill(ds, "stud");
            dt1 = ds.Tables["stud"];
            Console.WriteLine("--------------Student Records------------");
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                Console.WriteLine(dt1.Rows[i][0]);
                Console.WriteLine(dt1.Rows[i][1]);
                Console.WriteLine(dt1.Rows[i][2]);
                Console.WriteLine(dt1.Rows[i][3]);
                Console.WriteLine(dt1.Rows[i][4]);
                Console.WriteLine("-------------------------------------");
            }

            da_course = new SqlDataAdapter("select * from courses", con);
            SqlCommandBuilder cbCourses = new SqlCommandBuilder(da_course);
            da_course.Fill(ds, "course");
            dt2 = ds.Tables["course"];
            Console.WriteLine("--------------Course Records------------");
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                Console.WriteLine(dt2.Rows[i][0]);
                Console.WriteLine(dt2.Rows[i][1]);
                Console.WriteLine(dt2.Rows[i][2]);
                Console.WriteLine(dt2.Rows[i][3]);
                Console.WriteLine("-------------------------------------");
            }
        }


        //Task 3.2 – Modify course credits (Disconnected Mode)


        public void ModifyCourseTask3_2()
        {
            SqlConnection con = new SqlConnection("integrated security=true; database=ado_test_dec12; server=(localdb)\\mssqllocaldb");

            SqlDataAdapter da = new SqlDataAdapter("select courseid, coursename, credits, semester from courses", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "course");

            Console.WriteLine("enter course id to modify credits:");
            int cid = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("enter new credits value:");
            int newCredits = Convert.ToInt32(Console.ReadLine());

            foreach (DataRow row in ds.Tables["course"].Rows)
            {
                if ((int)row["courseid"] == cid)
                {
                    row["credits"] = newCredits; 
                    break;
                }
            }

            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Update(ds, "course");

            Console.WriteLine("credits updated successfully.");
        }


        //Task 3.3 – Insert a new course (Disconnected Mode)


        public void InsertCourseTask3_3()
        {
            SqlConnection con = new SqlConnection("integrated security=true; database=ado_test_dec12; server=(localdb)\\mssqllocaldb");

            SqlDataAdapter da = new SqlDataAdapter("select courseid, coursename, credits, semester from courses", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "course");

            Console.WriteLine("enter course name:");
            string cname = Console.ReadLine();

            Console.WriteLine("enter credits:");
            int credits = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("enter semester:");
            string sem = Console.ReadLine();

            DataRow newRow = ds.Tables["course"].NewRow();
            newRow["coursename"] = cname;
            newRow["credits"] = credits;
            newRow["semester"] = sem;
            ds.Tables["course"].Rows.Add(newRow);

            SqlCommandBuilder cb = new SqlCommandBuilder(da);

            da.Update(ds, "course");

            Console.WriteLine("new course inserted successfully.");
        }

        //Task 3.4 – Delete a student (Disconnected Mode)
        //Delete student record inside DataTable.

        public void DeleteStudentTask3_4()
        {
            SqlConnection con = new SqlConnection("Integrated security=true;database=ado_test_dec12;server=(localdb)\\MSSQLLocalDB");

            SqlDataAdapter da = new SqlDataAdapter("select * from Students", con);
            SqlCommandBuilder cmd = new SqlCommandBuilder(da); 

            DataSet ds = new DataSet(); 
            da.Fill(ds, "Students"); 
            DataTable dt = ds.Tables["Students"]; 
            dt.PrimaryKey = new DataColumn[] { dt.Columns["StudentId"] }; 
            Console.WriteLine("Enter Student ID to delete: ");
            int sid = Convert.ToInt32(Console.ReadLine());

            DataRow row = dt.Rows.Find(sid);
            if (row == null)
            {
                Console.WriteLine("Student not found!");
                return;
            }

            row.Delete();

            da.Update(ds, "Students");

            Console.WriteLine("Student deleted successfully");
        }


       
            
        








    }
}
