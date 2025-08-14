using System;

namespace InventoryManagementSystem.Models
{
  
    public record InventoryItem(int Id, string Name, int Quantity, DateTime DateAdded) : IInventoryEntity
    {
      
        public static InventoryItem Create(int id, string name, int quantity)
        {
            return new InventoryItem(id, name, quantity, DateTime.Now);
        }

       
        public InventoryItem WithQuantity(int newQuantity)
        {
            return this with { Quantity = newQuantity };
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Qty: {Quantity}, Added: {DateAdded:yyyy-MM-dd}";
        }
    }
}
