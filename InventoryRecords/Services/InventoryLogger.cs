using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Exceptions;

namespace InventoryManagementSystem.Services
{
    
    public class InventoryLogger<T> where T : IInventoryEntity
    {
        private List<T> _log;
        private readonly string _filePath;
        private readonly JsonSerializerOptions _jsonOptions;

   
        public int Count => _log.Count;

        
        public string FilePath => _filePath;

        public InventoryLogger(string filePath)
        {
            _log = new List<T>();
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            // Ensure directory exists
            var directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        
        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            // Check for duplicate IDs
            if (_log.Any(x => x.Id == item.Id))
                throw new InventoryOperationException($"An item with ID {item.Id} already exists in the log.");

            _log.Add(item);
            Console.WriteLine($"Added item with ID: {item.Id} to the log.");
        }

        public List<T> GetAll()
        {
            return new List<T>(_log);
        }

        
        public T GetById(int id)
        {
            return _log.FirstOrDefault(x => x.Id == id);
        }

        public bool Remove(int id)
        {
            var item = _log.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _log.Remove(item);
                Console.WriteLine($"Removed item with ID: {id} from the log.");
                return true;
            }
            return false;
        }

       
        public void SaveToFile()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(_log, _jsonOptions);
                
                using var writer = new StreamWriter(_filePath);
                writer.Write(jsonString);

                Console.WriteLine($"Successfully saved {_log.Count} items to file: {_filePath}");
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new InventoryFileException(_filePath, $"Access denied when saving to file: {_filePath}", ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new InventoryFileException(_filePath, $"Directory not found when saving to file: {_filePath}", ex);
            }
            catch (IOException ex)
            {
                throw new InventoryFileException(_filePath, $"IO error when saving to file: {_filePath}", ex);
            }
            catch (JsonException ex)
            {
                throw new InventoryDataException("JSON serialization error occurred.", ex);
            }
            catch (Exception ex)
            {
                throw new InventoryFileException(_filePath, $"Unexpected error when saving to file: {_filePath}", ex);
            }
        }

        
        public void LoadFromFile()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    Console.WriteLine($"File not found: {_filePath}. Starting with empty log.");
                    _log = new List<T>();
                    return;
                }

                using var reader = new StreamReader(_filePath);
                string jsonString = reader.ReadToEnd();
                
                if (string.IsNullOrWhiteSpace(jsonString))
                {
                    Console.WriteLine("File is empty. Starting with empty log.");
                    _log = new List<T>();
                    return;
                }

                _log = JsonSerializer.Deserialize<List<T>>(jsonString, _jsonOptions) ?? new List<T>();
                Console.WriteLine($"Successfully loaded {_log.Count} items from file: {_filePath}");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"File not found when loading: {ex.Message}");
                _log = new List<T>();
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new InventoryFileException(_filePath, $"Access denied when loading from file: {_filePath}", ex);
            }
            catch (IOException ex)
            {
                throw new InventoryFileException(_filePath, $"IO error when loading from file: {_filePath}", ex);
            }
            catch (JsonException ex)
            {
                throw new InventoryDataException("JSON deserialization error occurred.", ex);
            }
            catch (Exception ex)
            {
                throw new InventoryFileException(_filePath, $"Unexpected error when loading from file: {_filePath}", ex);
            }
        }


        public void ClearLog()
        {
            _log.Clear();
            Console.WriteLine("In-memory log cleared.");
        }

        
        public bool FileExists()
        {
            return File.Exists(_filePath);
        }
    }
}
