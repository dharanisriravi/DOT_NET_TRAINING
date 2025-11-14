using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Online_utility_billing
{
    internal class Customer
    {

        int customer_ID;
        string customer_Name;
        int total_readings;
        double total_usage;
        double tax;

        public Customer(int customer_ID, string customer_Name)
        {
            this.customer_ID = customer_ID; 
            this.customer_Name = customer_Name;

        }
        public void GetReadings(params int[] readings )
        {
            total_readings = 0;
            foreach (int read in readings)
            {
                total_readings += read;
            }

        }
        public void CalculateGST()
        {
            total_usage = total_readings * 6.5;
            tax=total_usage* 18/ 100;
        }
        public void DisplayDetails()
        {
            Console.WriteLine("=======CUSTOMER BILL DETAILS======");
            Console.WriteLine($"\nID: {customer_ID}");
            Console.WriteLine($"\nName: {customer_Name}");
            Console.WriteLine("\nService Charge: Rs.50");
            //Console.WriteLine($"\n Tax Applied : {tax}");
            Console.WriteLine($"\nTotal usage:{total_readings * 6.5}");
            Console.WriteLine($"\nTax Applied : {tax}");
            Console.WriteLine($"\nNet Payable: {50+tax+total_readings} units");
            Console.WriteLine("\n---------------------------------------------------");

        }

    }
}
