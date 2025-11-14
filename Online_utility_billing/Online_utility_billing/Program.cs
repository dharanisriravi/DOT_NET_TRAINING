using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_utility_billing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=======Welcome to Online Utility Billing System======");
           

            Console.WriteLine("\nEnter the no of customers");
            int no_of_cust=Convert.ToInt32(Console.ReadLine());
            Customer[] customer = new Customer[no_of_cust];

            for (int i=0;i<no_of_cust;i++)
            {
                Console.WriteLine("\n---------------------------------------------------");
                Console.WriteLine($"\nEnter details for customer {i + 1}:");
                Console.WriteLine("\n\"Enter ID: ");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("\n\"Enter Name: ");
                string name= Console.ReadLine();
                customer[i]=new Customer(id, name);
                Console.Write("\nEnter number of readings: ");
                int count = Convert.ToInt32(Console.ReadLine());
                int [] readings = new int[count];
                for (int j = 0; j < count; j++)
                {
                    Console.Write($"\nEnter reading {j + 1}: ");
                    readings[i] = Convert.ToInt32(Console.ReadLine());
                }
                customer[i].GetReadings(readings);
                 customer[i].CalculateGST();
                Console.WriteLine("\n---------------------------------------------------");

                customer[i].DisplayDetails();


            }
        }
    }
}
