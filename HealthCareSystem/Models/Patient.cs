namespace HealthCareSystem.Models
{
    // Patient class representing a healthcare patient
    public class Patient
    {
        public int Id { get; }
        public string Name { get; }
        public int Age { get; }
        public string Gender { get; }

        // Constructor that initializes all fields
        public Patient(int id, string name, int age, string gender)
        {
            Id = id;
            Name = name;
            Age = age;
            Gender = gender;
        }

        public override string ToString()
        {
            return $"Patient ID: {Id}, Name: {Name}, Age: {Age}, Gender: {Gender}";
        }
    }
}