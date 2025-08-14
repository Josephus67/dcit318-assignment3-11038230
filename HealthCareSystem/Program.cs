// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using System;

namespace HealthCareSystem
{
    // Main Program class for Healthcare System
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("HEALTHCARE SYSTEM");

            try
            {
                //  Instantiate HealthSystemApp
                var healthApp = new HealthSystemApp();

                //  Call SeedData()
                healthApp.SeedData();

                //  Call BuildPrescriptionMap()
                healthApp.BuildPrescriptionMap();

                //  Print all patients
                healthApp.PrintAllPatients();

                //  Select one PatientId and display all prescriptions for that patient
                int selectedPatientId = 1; 
                Console.WriteLine($"Selected Patient ID: {selectedPatientId}");
                
                var selectedPatient = healthApp.GetPatientById(selectedPatientId);
                if (selectedPatient != null)
                {
                    Console.WriteLine($"Selected Patient: {selectedPatient.Name}");
                }

                healthApp.PrintPrescriptionsForPatient(selectedPatientId);

                // prescriptions for another patient
                Console.WriteLine("Additional Demo");
                healthApp.PrintPrescriptionsForPatient(2); // Jane Smith

                Console.WriteLine("Healthcare system completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}