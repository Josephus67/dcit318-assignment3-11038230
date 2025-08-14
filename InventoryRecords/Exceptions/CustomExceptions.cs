using System;

namespace InventoryManagementSystem.Exceptions
{
    public abstract class InventoryException : Exception
    {
        protected InventoryException(string message) : base(message) { }
        protected InventoryException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class InventoryFileException : InventoryException
    {
        public string FilePath { get; }

        public InventoryFileException(string filePath, string message) : base(message)
        {
            FilePath = filePath;
        }

        public InventoryFileException(string filePath, string message, Exception innerException) 
            : base(message, innerException)
        {
            FilePath = filePath;
        }
    }

    public class InventoryDataException : InventoryException
    {
        public InventoryDataException(string message) : base(message) { }
        public InventoryDataException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class InventoryOperationException : InventoryException
    {
        public InventoryOperationException(string message) : base(message) { }
        public InventoryOperationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
