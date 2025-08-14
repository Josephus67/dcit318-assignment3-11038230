using System;

namespace FinanceManagement
{
    // Main Program class for Finance Management System
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("FINANCE MANAGEMENT SYSTEM");

            try
            {
                //  an instance of FinanceApp and run the system
                var financeApp = new FinanceApp();
                financeApp.Run();
                
                Console.WriteLine("Finance management system completed successfully!");
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