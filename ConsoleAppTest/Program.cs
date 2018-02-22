using System;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string subject = "1/6/2018 2:30:29 AM";

            var timex = subject.Split(null);

            var datex = DateTime.Parse(timex[0]);
            var time24 = DateTime.Parse(timex[1] + " " + timex[2]);

            Console.WriteLine(datex.ToString("dd/MM/yyyy"));
            Console.WriteLine(time24.ToString("HH:mm:ss"));
            Console.WriteLine(time24.ToString("HH:mm:ss") +" "+ datex.ToString("dd/MM/yyyy"));
            Console.ReadLine();
        }
    }
}
