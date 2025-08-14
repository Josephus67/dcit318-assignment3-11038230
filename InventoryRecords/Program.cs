using System;
using InventoryManagementSystem.Services;
using InventoryManagementSystem.Exceptions;

namespace InventoryManagementSystem
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" Inventory Management System ");
            Console.WriteLine("Using C# Records, Generics, and File Operations");
            Console.WriteLine("Organized with Clean Architecture\n");

            try
            {
                // Create an instance of InventoryApp with custom file path
                var app = new InventoryApp("Data/inventory_data.json");

                // Step 1: Seed sample data
                app.SeedSampleData();

                // Step 2: Print current items before saving
                app.PrintAllItems();

                // Step 3: Print inventory statistics
                app.PrintInventoryStats();

                // Step 4: Save data to persist to disk
                app.SaveData();

                // Step 5: Clear memory and simulate a new session
                app.ClearMemory();
                Console.WriteLine("Memory cleared - simulating application restart...\n");

                // Step 6: Load data from file
                app.LoadData();

                // Step 7: Print all items to confirm data was recovered
                app.PrintAllItems();

                // Step 8: Demonstrate record immutability
                app.DemonstrateRecordImmutability();

                Console.WriteLine("\n Application Completed Successfully ");
                Console.WriteLine($"Data persisted to: {app.GetLogger().FilePath}");
                Console.WriteLine($"File exists: {app.GetLogger().FileExists()}");
            }
            catch (InventoryException ex)
            {
                Console.WriteLine($"\nInventory system error: {ex.Message}");
                
                if (ex is InventoryFileException fileEx)
                {
                    Console.WriteLine($"File path: {fileEx.FilePath}");
                }
                
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nUnexpected application error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
