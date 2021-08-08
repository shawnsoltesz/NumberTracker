using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace NumberTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Number Tracker");

            var numbers = new List<int>();

            if (File.Exists("numbers.csv"))

            {
                var fileReader = new StreamReader("numbers.csv");

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)

                {
                    HasHeaderRecord = false,
                };

                var csvReader = new CsvReader(fileReader, config);

                numbers = csvReader.GetRecords<int>().ToList();

            }
            // Controls if we are still running our loop asking for more numbers
            var isRunning = true;

            // While we are running
            while (isRunning)
            {
                // Show the list of numbers
                Console.WriteLine("------------------");
                foreach (var number in numbers)
                {
                    Console.WriteLine(number);
                }
                Console.WriteLine($"Our list has: {numbers.Count()} entries");
                Console.WriteLine("------------------");

                // Ask for a new number or the word quit to end
                Console.Write("Enter a number to store, or 'quit' to end: ");
                var input = Console.ReadLine().ToLower();

                if (input == "quit")
                {
                    // If the input is quit, turn off the flag to keep looping
                    isRunning = false;
                }
                else
                {
                    // Parse the number and add it to the list of numbers
                    var number = int.Parse(input);
                    numbers.Add(number);
                }
            }

            var fileWriter = new StreamWriter("numbers.csv");
            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(numbers);
            fileWriter.Close();
        }

    }
}
