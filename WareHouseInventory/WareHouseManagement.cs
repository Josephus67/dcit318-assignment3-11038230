using System;
using System.Collections.Generic;
using WareHouseManagementSystem.Models;
using WareHouseManagementSystem.Repository;
using WareHouseManagementSystem.Interfaces;
using WareHouseManagementSystem.Exceptions;

namespace WareHouseManagementSystem
{
    // WareHouseManager class - manages both grocery and electronic inventory
    public class WareHouseManager
    {
        private InventoryRepository<ElectronicItem> _electronics = new InventoryRepository<ElectronicItem>();
        private InventoryRepository<GroceryItem> _groceries = new InventoryRepository<GroceryItem>();

        // Seed sample data items of each type
        public void SeedData()
        {
            Console.WriteLine("Seeding warehouse data...");

            try
            {
                // Add electronic items
                _electronics.AddItem(new ElectronicItem(1, "Laptop", 10, "Dell", 24));
                _electronics.AddItem(new ElectronicItem(2, "Smartphone", 25, "Samsung", 12));
                _electronics.AddItem(new ElectronicItem(3, "Tablet", 15, "Apple", 12));

                // Add grocery items
                _groceries.AddItem(new GroceryItem(1, "Milk", 50, DateTime.Now.AddDays(7)));
                _groceries.AddItem(new GroceryItem(2, "Bread", 30, DateTime.Now.AddDays(3)));
                _groceries.AddItem(new GroceryItem(3, "Eggs", 40, DateTime.Now.AddDays(14)));

                Console.WriteLine("Warehouse data seeded successfully.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding data: {ex.Message}");
            }
        }

        // Generic method to print all items in a repository
        public void PrintAllItems<T>(InventoryRepository<T> repo) where T : IInventoryItem
        {
            try
            {
                var items = repo.GetAllItems();
                if (items.Count == 0)
                {
                    Console.WriteLine("No items found in inventory.");
                }
                else
                {
                    foreach (var item in items)
                    {
                        Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error printing items: {ex.Message}");
            }
        }

        // Generic method to increase stock
        public void IncreaseStock<T>(InventoryRepository<T> repo, int id, int quantity) where T : IInventoryItem
        {
            try
            {
                var item = repo.GetItemById(id);
                int newQuantity = item.Quantity + quantity;
                repo.UpdateQuantity(id, newQuantity);
                Console.WriteLine($"Stock increased for item '{item.Name}' (ID: {id}). New quantity: {newQuantity}");
            }
            catch (ItemNotFoundException ex)
            {
                Console.WriteLine($"Cannot increase stock: {ex.Message}");
            }
            catch (InvalidQuantityException ex)
            {
                Console.WriteLine($"Invalid quantity operation: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error increasing stock: {ex.Message}");
            }
        }

        // Generic method to remove item by ID
        public void RemoveItemById<T>(InventoryRepository<T> repo, int id) where T : IInventoryItem
        {
            try
            {
                var item = repo.GetItemById(id); // Get item info before removing
                repo.RemoveItem(id);
                Console.WriteLine($"Item '{item.Name}' (ID: {id}) removed successfully from inventory.");
            }
            catch (ItemNotFoundException ex)
            {
                Console.WriteLine($"Cannot remove item: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error removing item: {ex.Message}");
            }
        }

        // Method to demonstrate error scenarios
        public void DemonstrateErrorScenarios()
        {
            Console.WriteLine(" DEMONSTRATING ERROR SCENARIOS ");

            // Trying to add a duplicate item
            Console.WriteLine("1. Testing duplicate item addition:");
            try
            {
                _electronics.AddItem(new ElectronicItem(1, "Duplicate Laptop", 5, "HP", 12));
            }
            catch (DuplicateItemException ex)
            {
                Console.WriteLine($" Caught expected error: {ex.Message}");
            }

            // Trying to remove a non-existent item
            Console.WriteLine("\n2. Testing removal of non-existent item:");
            try
            {
                _groceries.RemoveItem(999);
            }
            catch (ItemNotFoundException ex)
            {
                Console.WriteLine($" Caught expected error: {ex.Message}");
            }

            // Trying to update with invalid quantity
            Console.WriteLine("\n3. Testing invalid quantity update:");
            try
            {
                _electronics.UpdateQuantity(1, -5);
            }
            catch (InvalidQuantityException ex)
            {
                Console.WriteLine($" Caught expected error: {ex.Message}");
            }

            Console.WriteLine();
        }

        // Get electronic items repository (for external access if needed)
        public InventoryRepository<ElectronicItem> GetElectronicsRepository()
        {
            return _electronics;
        }

        // Get grocery items repository (for external access if needed)
        public InventoryRepository<GroceryItem> GetGroceriesRepository()
        {
            return _groceries;
        }
    }
}