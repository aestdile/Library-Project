
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace LibraryManagementSystem
{
    public static class DataManager
    {
        public static void Initialize(ref List<Book> books, ref List<Reader> readers, string booksFilePath, string readersFilePath)
        {
            LoadData(ref books, ref readers, booksFilePath, readersFilePath);
        }

        public static void LoadData(ref List<Book> books, ref List<Reader> readers, string booksFilePath, string readersFilePath)
        {
            try
            {
                if (File.Exists(booksFilePath))
                {
                    string booksJson = File.ReadAllText(booksFilePath);
                    books = JsonSerializer.Deserialize<List<Book>>(booksJson);
                }
                else
                {
                    InitializeSampleBooks(ref books, booksFilePath, readersFilePath);
                }

                if (File.Exists(readersFilePath))
                {
                    string readersJson = File.ReadAllText(readersFilePath);
                    readers = JsonSerializer.Deserialize<List<Reader>>(readersJson);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading data: " + ex.Message);
                InitializeSampleBooks(ref books, booksFilePath, readersFilePath);
            }
        }

        public static void SaveData(List<Book> books, List<Reader> readers, string booksFilePath, string readersFilePath)
        {
            try
            {
                string booksJson = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(booksFilePath, booksJson);

                string readersJson = JsonSerializer.Serialize(readers, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(readersFilePath, readersJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving data: " + ex.Message);
            }
        }

        private static void InitializeSampleBooks(ref List<Book> books, string booksFilePath, string readersFilePath)
        {
            books = new List<Book>
            {
                new Book { Id = 1, Title = "CodeBreaker", Author = "Uolter Ayzekson", Genre = "Biografiya, Ilm-fan, Texnologiya tarixi", TotalQuantity = 5, AvailableQuantity = 5, Readers = new List<string>() },
                new Book { Id = 2, Title = "Mahorat", Author = "Robert Grin", Genre = "Shaxsiy rivojlanish, Psixologiya, Motivatsiya", TotalQuantity = 3, AvailableQuantity = 3, Readers = new List<string>() },
                new Book { Id = 3, Title = "Rejali Ayol", Author = "Mey Mask", Genre = "Biografiya, Shaxsiy rivojlanish", TotalQuantity = 4, AvailableQuantity = 4, Readers = new List<string>() },
                new Book { Id = 4, Title = "1984", Author = "George Orwell", Genre = " Distopiya, Ilmiy-fantastika, Siyosiy fantastika", TotalQuantity = 2, AvailableQuantity = 2, Readers = new List<string>() },
                new Book { Id = 5, Title = "Bank 4.0", Author = "Brett King", Genre = "Moliya, Fintech, Biznes va Texnologiya", TotalQuantity = 3, AvailableQuantity = 3, Readers = new List<string>() }
            };
            SaveData(books, new List<Reader>(), booksFilePath, readersFilePath);
        }
    }
}











