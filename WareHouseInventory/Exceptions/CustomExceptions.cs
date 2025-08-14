using System;

namespace WareHouseManagementSystem.Exceptions
{
    // Custom exception for duplicate items
    public class DuplicateItemException : Exception
    {
        public DuplicateItemException(string message) : base(message) { }
    }

    // Custom exception for items not found
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string message) : base(message) { }
    }

    // Custom exception for invalid quantities
    public class InvalidQuantityException : Exception
    {
        public InvalidQuantityException(string message) : base(message) { }
    }
}