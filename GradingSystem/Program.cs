// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using System;
using System.IO;
using GradingSystem.Exceptions;

namespace GradingSystem
{
    // Main Program class for Question 4: Student Grading System
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("QUESTION 4: STUDENT GRADING SYSTEM");

            // File paths
            string inputFilePath = "students.txt";
            string outputFilePath = "grade_report.txt";

            // Wrap the entire process in a try-catch block
            try
            {
                var processor = new StudentResultProcessor();

                // Create sample input file for demonstration
                Console.WriteLine("Creating sample input file...");
                processor.CreateSampleInputFile(inputFilePath);
                Console.WriteLine();

                // Display input file contents
                Console.WriteLine("Input file contents:");
                if (File.Exists(inputFilePath))
                {
                    using (var reader = new StreamReader(inputFilePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                        }
                    }
                }
                Console.WriteLine();

                // Main processing flow
                Console.WriteLine("Processing student data...");

                // Call ReadStudentsFromFile(...) and pass the input file path
                var students = processor.ReadStudentsFromFile(inputFilePath);
                Console.WriteLine($"Successfully read {students.Count} student records.\n");

                // Display processed results to console
                Console.WriteLine("Processed Student Results:");
                foreach (var student in students)
                {
                    Console.WriteLine(student.ToString());
                }
                Console.WriteLine();

                // Call WriteReportToFile(...) and pass the student list and output file path
                processor.WriteReportToFile(students, outputFilePath);
                Console.WriteLine();

                // Display output file contents
                Console.WriteLine("Generated report contents:");
                if (File.Exists(outputFilePath))
                {
                    using (var reader = new StreamReader(outputFilePath))
                    {
                        Console.WriteLine(reader.ReadToEnd());
                    }
                }

                Console.WriteLine("Student grading system completed successfully!");
            }
            // Catch and display specific exceptions
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($" File Error: Input file is missing.");
                Console.WriteLine($" Details: {ex.Message}");
                Console.WriteLine($" Please ensure the file '{inputFilePath}' exists in the application directory.");
            }
            catch (InvalidScoreFormatException ex)
            {
                Console.WriteLine($" Score Format Error: {ex.Message}");
                Console.WriteLine($"   Please check that all scores are valid integers.");
            }
            catch (MissingFieldException ex)
            {
                Console.WriteLine($" Missing Field Error: {ex.Message}");
                Console.WriteLine($"   Please ensure each line has exactly 3 comma-separated values: ID,Name,Score");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($" Access Error: Cannot access file.");
                Console.WriteLine($" Details: {ex.Message}");
                Console.WriteLine($" Please check file permissions and ensure files are not locked by other applications.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($" I/O Error: {ex.Message}");
                Console.WriteLine($"   Please check disk space and file accessibility.");
            }
            catch (Exception ex)
            {
                // Catch any other unforeseen errors
                Console.WriteLine($" Unexpected Error: {ex.Message}");
                Console.WriteLine($" Type: {ex.GetType().Name}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
