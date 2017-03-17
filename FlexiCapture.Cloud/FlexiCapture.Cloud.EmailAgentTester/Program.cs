using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexiCapture.Cloud.EmailAgentTester.Helpers;

namespace FlexiCapture.Cloud.EmailAgentTester
{
    class Program
    {
        /// <summary>
        /// main method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Run tests...");
                TesterHelper.RunTests();
                Console.WriteLine("Tests completed...");
                Console.ReadKey();
            }
            catch (Exception)
            {
                Console.ReadKey();
            }
        }
    }
}
