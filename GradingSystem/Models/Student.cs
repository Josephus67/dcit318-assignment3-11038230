namespace GradingSystem.Models
{
    // Student Class with grade calculation
    public class Student
    {
        public int Id { get; }
        public string FullName { get; }
        public int Score { get; }

        public Student(int id, string fullName, int score)
        {
            Id = id;
            FullName = fullName;
            Score = score;
        }

        // GetGrade method - assigns grades based on score ranges
        public string GetGrade()
        {
            return Score switch
            {
                >= 80 and <= 100 => "A",
                >= 70 and < 80 => "B",
                >= 60 and < 70 => "C",
                >= 50 and < 60 => "D",
                _ => "F"
            };
        }

        public override string ToString()
        {
            return $"{FullName} (ID: {Id}): Score = {Score}, Grade = {GetGrade()}";
        }
    }
}