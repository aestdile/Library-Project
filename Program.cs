
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace LibraryManagementSystem
{
    class Program
    {
        static List<Book> books = new List<Book>();
        static List<Reader> readers = new List<Reader>();
        static Reader currentReader = null;
        static string booksFilePath = "books.json";
        static string readersFilePath = "readers.json";

        static void Main(string[] args)
        {
            DataManager.Initialize(ref books, ref readers, booksFilePath, readersFilePath);
            
            while (true)
            {
                if (currentReader == null)
                {
                    AuthManager.ShowLoginMenu(ref readers, ref currentReader, booksFilePath, readersFilePath);
                }
                else
                {
                    BookManager.ShowMainMenu(ref books, ref readers, ref currentReader, booksFilePath, readersFilePath);
                }
            }
        }
    }
}























