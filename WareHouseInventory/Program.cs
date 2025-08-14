// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using System;

namespace WareHouseManagementSystem
{
    // Main Program class for Question 3: Warehouse Inventory Management System
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("WAREHOUSE INVENTORY MANAGEMENT SYSTEM");

            try
            {
                // Instantiate WareHouseManager
                var warehouseManager = new WareHouseManager();

                // Call SeedData()
                warehouseManager.SeedData();

                // Print all grocery items
                Console.WriteLine(" ALL GROCERY ITEMS ");
                warehouseManager.PrintAllItems(warehouseManager.GetGroceriesRepository());
                Console.WriteLine();

                // Print all electronic items
                Console.WriteLine("ALL ELECTRONIC ITEMS ");
                warehouseManager.PrintAllItems(warehouseManager.GetElectronicsRepository());
                Console.WriteLine();

                // Try error scenarios - each should be caught and displayed with proper error messages
                warehouseManager.DemonstrateErrorScenarios();

                // Additional demonstrations
                Console.WriteLine("MORE OPERATIONS ");
                
                // Demonstrate successful operations
                Console.WriteLine("Testing successful stock increase:");
                warehouseManager.IncreaseStock(warehouseManager.GetElectronicsRepository(), 1, 5);
                
                Console.WriteLine("\nTesting successful item removal:");
                warehouseManager.RemoveItemById(warehouseManager.GetGroceriesRepository(), 3);

                Console.WriteLine("\nFinal inventory state:");
                Console.WriteLine(" Electronics ");
                warehouseManager.PrintAllItems(warehouseManager.GetElectronicsRepository());
                Console.WriteLine(" Groceries ");
                warehouseManager.PrintAllItems(warehouseManager.GetGroceriesRepository());

                Console.WriteLine("\nWarehouse inventory system completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}