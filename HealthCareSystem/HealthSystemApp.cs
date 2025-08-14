using System;
using System.Collections.Generic;
using System.Linq;
using HealthCareSystem.Models;
using HealthCareSystem.Repository;

namespace HealthCareSystem
{
    // Health System Application - manages patients and prescriptions
    public class HealthSystemApp
    {
        private Repository<Patient> _patientRepo = new Repository<Patient>();
        private Repository<Prescription> _prescriptionRepo = new Repository<Prescription>();
        private Dictionary<int, List<Prescription>> _prescriptionMap = new Dictionary<int, List<Prescription>>();

        // Seed sample data into the repositories
        public void SeedData()
        {
            Console.WriteLine("Seeding sample data...");

            // Adding Patient objects to the patient repository
            _patientRepo.Add(new Patient(1, "Bawah Josephus", 89, "Male"));
            _patientRepo.Add(new Patient(2, "Paul Ammah", 28, "Male"));
            _patientRepo.Add(new Patient(3, "Justice Appati", 42, "Male"));
            _patientRepo.Add(new Patient(3, "Juliet Ibrahim", 38, "Female"));


            // Adding Prescription objects to the prescription repository (with valid PatientIds)
            _prescriptionRepo.Add(new Prescription(1, 1, "Aspirin", DateTime.Now.AddDays(-5)));
            _prescriptionRepo.Add(new Prescription(2, 1, "Ibuprofen", DateTime.Now.AddDays(-3)));
            _prescriptionRepo.Add(new Prescription(3, 2, "Paracetamol", DateTime.Now.AddDays(-7)));
            _prescriptionRepo.Add(new Prescription(4, 3, "Amoxicillin", DateTime.Now.AddDays(-2)));
            _prescriptionRepo.Add(new Prescription(5, 2, "Vitamin D", DateTime.Now.AddDays(-1)));

            Console.WriteLine("Sample data seeded successfully.\n");
        }

        // Build prescription map by grouping prescriptions by PatientId
        public void BuildPrescriptionMap()
        {
            Console.WriteLine("Building prescription map...");
            
            var prescriptions = _prescriptionRepo.GetAll();
            _prescriptionMap.Clear();

            // Loop through all prescriptions and group them by PatientId
            foreach (var prescription in prescriptions)
            {
                if (!_prescriptionMap.ContainsKey(prescription.PatientId))
                {
                    _prescriptionMap[prescription.PatientId] = new List<Prescription>();
                }
                _prescriptionMap[prescription.PatientId].Add(prescription);
            }

            Console.WriteLine($"Prescription map built with {_prescriptionMap.Count} patient groups.\n");
        }

        // Print all patients using the repository
        public void PrintAllPatients()
        {
            Console.WriteLine(" ALL PATIENTS ");
            var patients = _patientRepo.GetAll();
            
            if (patients.Count == 0)
            {
                Console.WriteLine("No patients found.");
            }
            else
            {
                foreach (var patient in patients)
                {
                    Console.WriteLine($"ID: {patient.Id}, Name: {patient.Name}, Age: {patient.Age}, Gender: {patient.Gender}");
                }
            }
            Console.WriteLine();
        }

        // Print all prescriptions for a specific patient using the map
        public void PrintPrescriptionsForPatient(int patientId)
        {
            Console.WriteLine($" PRESCRIPTIONS FOR PATIENT ID {patientId} ");
            
            if (_prescriptionMap.ContainsKey(patientId))
            {
                var prescriptions = _prescriptionMap[patientId];
                foreach (var prescription in prescriptions)
                {
                    Console.WriteLine($"ID: {prescription.Id}, Medication: {prescription.MedicationName}, Date: {prescription.DateIssued:yyyy-MM-dd}");
                }
                Console.WriteLine($"Total prescriptions: {prescriptions.Count}");
            }
            else
            {
                Console.WriteLine("No prescriptions found for this patient.");
            }
            Console.WriteLine();
        }

        // Get prescriptions by patient ID from the dictionary
        public List<Prescription> GetPrescriptionsByPatientId(int patientId)
        {
            return _prescriptionMap.ContainsKey(patientId) 
                ? _prescriptionMap[patientId] 
                : new List<Prescription>();
        }

        // Get patient by ID using the repository
        public Patient? GetPatientById(int patientId)
        {
            return _patientRepo.GetById(p => p.Id == patientId);
        }
    }
}