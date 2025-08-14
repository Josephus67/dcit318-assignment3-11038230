using System;
using System.Collections.Generic;
using System.IO;
using GradingSystem.Models;
using GradingSystem.Exceptions;

namespace GradingSystem
{
    // StudentResultProcessor Class - handles file operations and data processing
    public class StudentResultProcessor
    {
        // Read students from input file with validation
        public List<Student> ReadStudentsFromFile(string inputFilePath)
        {
            var students = new List<Student>();
            int lineNumber = 0;

            // Use using statement with StreamReader to read the file line by line
            using (var reader = new StreamReader(inputFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lineNumber++;
                    
                    // Skip empty lines
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    try
                    {
                        // Split each line by comma
                        var fields = line.Split(',');
                        
                        // Validate the number of fields
                        if (fields.Length != 3)
                        {
                            throw new MissingFieldException($"Line {lineNumber}: '{line}' has {fields.Length} fields. Expected 3 fields (ID, Name, Score).");
                        }

                        // Parse and validate ID
                        string idString = fields[0].Trim();
                        if (!int.TryParse(idString, out int id))
                        {
                            throw new InvalidScoreFormatException($"Line {lineNumber}: Invalid ID format '{idString}'. ID must be a valid integer.");
                        }

                        // Extract and validate name
                        string fullName = fields[1].Trim();
                        if (string.IsNullOrWhiteSpace(fullName))
                        {
                            throw new MissingFieldException($"Line {lineNumber}: Name field is empty or contains only whitespace.");
                        }

                        // Try converting the score to an integer
                        string scoreString = fields[2].Trim();
                        if (!int.TryParse(scoreString, out int score))
                        {
                            // If parsing fails, throw InvalidScoreFormatException
                            throw new InvalidScoreFormatException($"Line {lineNumber}: Invalid score format '{scoreString}'. Score must be a valid integer.");
                        }

                        // Validate score range (optional - but good practice)
                        if (score < 0 || score > 100)
                        {
                            Console.WriteLine($"Warning: Line {lineNumber}: Score {score} is outside typical range (0-100).");
                        }

                        // Store each valid student in List<Student>
                        students.Add(new Student(id, fullName, score));
                    }
                    catch (InvalidScoreFormatException)
                    {
                        // Re-throw custom exceptions
                        throw;
                    }
                    catch (MissingFieldException)
                    {
                        // Re-throw custom exceptions  
                        throw;
                    }
                    catch (Exception ex)
                    {
                        // Wrap any other exceptions
                        throw new Exception($"Line {lineNumber}: Unexpected error processing '{line}': {ex.Message}", ex);
                    }
                }
            }

            return students;
        }

        // Write formatted report to output file
        public void WriteReportToFile(List<Student> students, string outputFilePath)
        {
            // Use using with StreamWriter
            using (var writer = new StreamWriter(outputFilePath))
            {
                // Write header
                writer.WriteLine("STUDENT GRADE REPORT");
                writer.WriteLine($"Generated on: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                writer.WriteLine($"Total students: {students.Count}");
                writer.WriteLine();

                if (students.Count == 0)
                {
                    writer.WriteLine("No student records found.");
                }
                else
                {
                    // Loop through each student
                    foreach (var student in students)
                    {
                        // Write a formatted summary
                        // Example: "Alice Smith (ID: 101): Score = 84, Grade = A"
                        writer.WriteLine($"{student.FullName} (ID: {student.Id}): Score = {student.Score}, Grade = {student.GetGrade()}");
                    }

                    // Write summary statistics
                    writer.WriteLine();
                    writer.WriteLine("SUMMARY STATISTICS");
                    
                    var gradeCount = new Dictionary<string, int>();
                    foreach (var student in students)
                    {
                        string grade = student.GetGrade();
                        gradeCount[grade] = gradeCount.ContainsKey(grade) ? gradeCount[grade] + 1 : 1;
                    }

                    foreach (var kvp in gradeCount)
                    {
                        writer.WriteLine($"Grade {kvp.Key}: {kvp.Value} student(s)");
                    }
                }
            }

            Console.WriteLine($"Report successfully written to: {outputFilePath}");
        }

        // Helper method to create sample input file for testing
        public void CreateSampleInputFile(string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("101,Bawah Josephus,84");
                writer.WriteLine("102,Paul Ammah,67");
                writer.WriteLine("103,Justice Appati,92");
                writer.WriteLine("104,Juliet Ibrahim,45");
                writer.WriteLine("105,Ariel Helwani,76");
            }
            Console.WriteLine($"Sample input file created: {filePath}");
        }
    }
}