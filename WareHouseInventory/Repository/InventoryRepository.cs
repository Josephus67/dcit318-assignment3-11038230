using System.Collections.Generic;
using System.Linq;
using WareHouseManagementSystem.Interfaces;
using WareHouseManagementSystem.Exceptions;

namespace WareHouseManagementSystem.Repository
{
    // Generic Inventory Repository with constraint
    public class InventoryRepository<T> where T : IInventoryItem
    {
        // Dictionary using item ID as the key
        private Dictionary<int, T> _items = new Dictionary<int, T>();

        // Add item to inventory
        public void AddItem(T item)
        {
            // Throw DuplicateItemException if the ID already exists
            if (_items.ContainsKey(item.Id))
            {
                throw new DuplicateItemException($"Item with ID {item.Id} already exists in inventory.");
            }
            _items[item.Id] = item;
        }

        // Get item by ID
        public T GetItemById(int id)
        {
            // Throw ItemNotFoundException if the item is not found
            if (!_items.ContainsKey(id))
            {
                throw new ItemNotFoundException($"Item with ID {id} not found in inventory.");
            }
            return _items[id];
        }

        // Remove item by ID
        public void RemoveItem(int id)
        {
            // Throw ItemNotFoundException if item not found
            if (!_items.ContainsKey(id))
            {
                throw new ItemNotFoundException($"Cannot remove item with ID {id}. Item not found in inventory.");
            }
            _items.Remove(id);
        }

        // Get all items
        public List<T> GetAllItems()
        {
            return _items.Values.ToList();
        }

        // Update quantity for an item
        public void UpdateQuantity(int id, int newQuantity)
        {
            // Throw InvalidQuantityException if quantity is negative
            if (newQuantity < 0)
            {
                throw new InvalidQuantityException("Quantity cannot be negative.");
            }

            // Throw ItemNotFoundException if item not found
            if (!_items.ContainsKey(id))
            {
                throw new ItemNotFoundException($"Cannot update quantity for item with ID {id}. Item not found in inventory.");
            }

            _items[id].Quantity = newQuantity;
        }

        // Get count of items in inventory
        public int GetItemCount()
        {
            return _items.Count;
        }

        // Check if item exists
        public bool ItemExists(int id)
        {
            return _items.ContainsKey(id);
        }
    }
}