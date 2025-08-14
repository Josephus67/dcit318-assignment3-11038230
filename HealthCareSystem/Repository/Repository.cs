using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCareSystem.Repository
{
    // Generic Repository class for entity storage and retrieval
    public class Repository<T>
    {
        private List<T> items = new List<T>();

        // Add an item to the repository
        public void Add(T item)
        {
            items.Add(item);
        }

        // Get all items from the repository
        public List<T> GetAll()
        {
            return items;
        }

        // Get first item that matches the predicate, or null if not found
        public T? GetById(Func<T, bool> predicate)
        {
            return items.FirstOrDefault(predicate);
        }

        // Remove item that matches the predicate
        public bool Remove(Func<T, bool> predicate)
        {
            var item = items.FirstOrDefault(predicate);
            if (item != null)
            {
                items.Remove(item);
                return true;
            }
            return false;
        }
    }
}