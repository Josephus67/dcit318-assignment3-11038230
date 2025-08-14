using System;

namespace GradingSystem.Exceptions
{
    // Custom exception for invalid score format
    // Triggered if the score cannot be converted to an integer
    public class InvalidScoreFormatException : Exception
    {
        public InvalidScoreFormatException(string message) : base(message) { }
    }

    // Custom exception for missing fields
    public class StudentMissingFieldException : Exception
    {
        public StudentMissingFieldException(string message) : base(message) { }
    }
}