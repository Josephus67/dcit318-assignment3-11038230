using System;

namespace HealthCareSystem.Models
{
    // Prescription class representing a medical prescription
    public class Prescription
    {
        public int Id { get; }
        public int PatientId { get; }
        public string MedicationName { get; }
        public DateTime DateIssued { get; }

        // Constructor that initializes all fields
        public Prescription(int id, int patientId, string medicationName, DateTime dateIssued)
        {
            Id = id;
            PatientId = patientId;
            MedicationName = medicationName;
            DateIssued = dateIssued;
        }

        public override string ToString()
        {
            return $"Prescription ID: {Id}, Patient ID: {PatientId}, Medication: {MedicationName}, Date: {DateIssued:yyyy-MM-dd}";
        }
    }
}