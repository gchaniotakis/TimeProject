using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TimeProject {
    class Program {
        static void Main(string[] args) {
            
            Console.WriteLine("Hello there!");

            Thread.Sleep(5000);

            Console.WriteLine("Looking for existing data file...");
            bool dataFileExists = SearchForDataFile();

            //no data file found, the user must choose either to calculate or quit the program.
            if (!dataFileExists) {
                Console.WriteLine("No data file found!");
                Thread.Sleep(3000);
                bool attempt = false;
                //loop for invalid syntax.
                while (!attempt) {
                    Console.WriteLine("Do you want to calculate? (Y/N)");
                    string command = Console.ReadLine();
                    attempt = HandleYNCommands(command, DateTimeCalculator.CalculateDateTimeStats);
                }


                //search again for data file (if the user typed Y then it should be there, if not the program must shut down.
                dataFileExists = SearchForDataFile();
                if (!dataFileExists) {
                    Console.WriteLine("There is nothing left to do without data!!");
                    Thread.Sleep(2000);
                    Console.WriteLine("Press any button to close...");
                    Console.Read();
                }
            }            
        }

        static bool HandleYNCommands(string command, Action<DateTime?, DateTime?> action, DateTime? begin = null, DateTime? end = null) {
            switch (command) {
                case "y":
                case "Y":
                    action(begin, end);
                    Console.WriteLine("Calculation completed!");
                    Thread.Sleep(1000);                    
                    return true;
                case "n":
                case "N":
                    return true;
                default:
                    Console.WriteLine("Incorrect Syntax");
                    return false;
            }
        }

        static bool SearchForDataFile() {
            return Directory.GetFiles(Directory.GetCurrentDirectory()).Where(x => x.Contains(DateTimeCalculator.FileName)).Any();
        }
    }
}
