using System;
using System.Collections.Generic;
using System.Linq;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Exceptions;

namespace InventoryManagementSystem.Services
{
    public class InventoryApp
    {
        private InventoryLogger<InventoryItem> _logger;

        public InventoryApp(string filePath = "Data/inventory.json")
        {
            _logger = new InventoryLogger<InventoryItem>(filePath);
        }

        public void SeedSampleData()
        {
            Console.WriteLine("\n Seeding Sample Data ");
            
            var sampleItems = new List<InventoryItem>
            {
                new InventoryItem(1, "Gaming Laptop", 25, DateTime.Now.AddDays(-30)),
                new InventoryItem(2, "Wireless Mouse", 150, DateTime.Now.AddDays(-15)),
                new InventoryItem(3, "USB-C Cable", 200, DateTime.Now.AddDays(-10)),
                new InventoryItem(4, "4K Monitor", 50, DateTime.Now.AddDays(-5)),
                new InventoryItem(5, "Mechanical Keyboard", 75, DateTime.Now.AddDays(-2))
            };

            try
            {
                foreach (var item in sampleItems)
                {
                    _logger.Add(item);
                }
                Console.WriteLine($"Successfully added {sampleItems.Count} sample items to the inventory.");
            }
            catch (InventoryOperationException ex)
            {
                Console.WriteLine($"Error adding sample data: {ex.Message}");
                throw;
            }
        }

        public void SaveData()
        {
            Console.WriteLine("\n Saving Data to File ");
            try
            {
                _logger.SaveToFile();
            }
            catch (InventoryException ex)
            {
                Console.WriteLine($"Failed to save data: {ex.Message}");
                throw;
            }
        }

        public void LoadData()
        {
            Console.WriteLine("\n Loading Data from File ");
            try
            {
                _logger.LoadFromFile();
            }
            catch (InventoryException ex)
            {
                Console.WriteLine($"Failed to load data: {ex.Message}");
                throw;
            }
        }

        
        public void PrintAllItems()
        {
            Console.WriteLine("\n Current Inventory Items ");
            var items = _logger.GetAll();

            if (items.Count == 0)
            {
                Console.WriteLine("No items found in inventory.");
                return;
            }

            // Print header
            Console.WriteLine($"{"ID",-5} {"Name",-25} {"Quantity",-10} {"Date Added",-12}");
            Console.WriteLine(new string('-', 55));

            // Print items sorted by ID
            foreach (var item in items.OrderBy(x => x.Id))
            {
                Console.WriteLine($"{item.Id,-5} {item.Name,-25} {item.Quantity,-10} {item.DateAdded:yyyy-MM-dd}");
            }

            // Print summary
            Console.WriteLine(new string('-', 55));
            Console.WriteLine($"Total items: {items.Count}");
            Console.WriteLine($"Total quantity: {items.Sum(x => x.Quantity):N0}");
        }

     
        public void ClearMemory()
        {
            Console.WriteLine("\n Clearing Memory (Simulating New Session) ");
            _logger.ClearLog();
        }

        public void DemonstrateRecordImmutability()
        {
            Console.WriteLine("\n Demonstrating Record Immutability ");
            var items = _logger.GetAll();
            
            if (items.Count == 0)
            {
                Console.WriteLine("No items available for demonstration.");
                return;
            }

            var originalItem = items[0];
            var modifiedItem = originalItem.WithQuantity(originalItem.Quantity + 10);
            var anotherModifiedItem = originalItem with { Name = originalItem.Name + " (Updated)" };
            
            Console.WriteLine($"Original:  {originalItem}");
            Console.WriteLine($"Modified:  {modifiedItem}");
            Console.WriteLine($"Another:   {anotherModifiedItem}");
            Console.WriteLine("Notice: Original record remains unchanged (immutable)");
        }

        public InventoryLogger<InventoryItem> GetLogger()
        {
            return _logger;
        }

        public void PrintInventoryStats()
        {
            var items = _logger.GetAll();
            
            if (items.Count == 0)
            {
                Console.WriteLine("No inventory statistics available.");
                return;
            }

            Console.WriteLine("\n Inventory Statistics ");
            Console.WriteLine($"Total Items: {items.Count}");
            Console.WriteLine($"Total Quantity: {items.Sum(x => x.Quantity):N0}");
            Console.WriteLine($"Average Quantity: {items.Average(x => x.Quantity):F2}");
            Console.WriteLine($"Highest Quantity: {items.Max(x => x.Quantity):N0}");
            Console.WriteLine($"Lowest Quantity: {items.Min(x => x.Quantity):N0}");
            Console.WriteLine($"Most Recent Addition: {items.Max(x => x.DateAdded):yyyy-MM-dd}");
            Console.WriteLine($"Oldest Addition: {items.Min(x => x.DateAdded):yyyy-MM-dd}");
        }
    }
}
