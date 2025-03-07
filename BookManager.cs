

using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem
{
    public static class BookManager
    {
        public static void ShowMainMenu(ref List<Book> books, ref List<Reader> readers, ref Reader currentReader, string booksFilePath, string readersFilePath)
        {
            Console.Clear();
            Console.WriteLine($"========= KUTUBXONA MENYU DASTURIGA XUSH KELIBSIZ! =========");
            Console.WriteLine($"Foydalanuvchi: {currentReader.FirstName} {currentReader.LastName}");
            Console.WriteLine($"Olingan kitoblar soni: {currentReader.BorrowedBooksCount}");
            Console.WriteLine("1. Kitoblar ro'yxatini ko'rish");
            Console.WriteLine("2. Kitob haqida ma'lumot olish");
            Console.WriteLine("3. Kitob olish");
            Console.WriteLine("4. Kitob qaytarish");
            Console.WriteLine("5. Mening kitoblarim");
            Console.WriteLine("6. Tizimdan chiqish");
            Console.WriteLine("====================================");
            Console.Write("Tanlovingizni kiriting: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        ShowBookList(books);
                        break;
                    case 2:
                        GetBookInfo(books);
                        break;
                    case 3:
                        BorrowBook(ref books, ref currentReader, booksFilePath, readersFilePath, readers);
                        break;
                    case 4:
                        ReturnBook(ref books, ref currentReader, booksFilePath, readersFilePath, readers);
                        break;
                    case 5:
                        ShowMyBooks(books, currentReader);
                        break;
                    case 6:
                        currentReader = null;
                        break;
                    default:
                        Console.WriteLine("Noto'g'ri tanlov! Davom etish uchun Enter tugmasini bosing...");
                        Console.ReadLine();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Noto'g'ri format! Davom etish uchun Enter tugmasini bosing...");
                Console.ReadLine();
            }
        }

        public static void ShowBookList(List<Book> books)
        {
            Console.Clear();
            Console.WriteLine("========= KITOBLAR RO'YXATI =========");
            
            if (books.Count == 0)
            {
                Console.WriteLine("Kutubxonada kitoblar mavjud emas.");
            }
            else
            {
                for (int i = 0; i < books.Count; i++)
                {
                    Book book = books[i];
                    Console.WriteLine($"{book.Id}. {book.Title} - {book.Author} ({book.AvailableQuantity}/{book.TotalQuantity})");
                }
            }
            
            Console.WriteLine("====================================");
            Console.WriteLine("Asosiy menyuga qaytish uchun Enter tugmasini bosing...");
            Console.ReadLine();
        }

        public static void GetBookInfo(List<Book> books)
        {
            Console.Clear();
            Console.WriteLine("========= KITOB HAQIDA MA'LUMOT =========");
            ShowBookList(books);
            
            Console.Write("Kitob raqamini kiriting: ");
            if (int.TryParse(Console.ReadLine(), out int bookId))
            {
                Book book = books.FirstOrDefault(b => b.Id == bookId);
                if (book != null)
                {
                    Console.WriteLine($"\nId: {book.Id}");
                    Console.WriteLine($"Nomi: {book.Title}");
                    Console.WriteLine($"Muallif: {book.Author}");
                    Console.WriteLine($"Janr: {book.Genre}");
                    Console.WriteLine($"Umumiy soni: {book.TotalQuantity}");
                    Console.WriteLine($"Mavjud soni: {book.AvailableQuantity}");
                    
                    if (book.Readers.Count > 0)
                    {
                        Console.WriteLine("Bu kitobni o'qiyotganlar:");
                        foreach (string readerName in book.Readers)
                        {
                            Console.WriteLine($"- {readerName}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Bu kitobni hozircha hech kim olmagan.");
                    }
                }
                else
                {
                    Console.WriteLine("Bunday raqamli kitob topilmadi!");
                }
            }
            else
            {
                Console.WriteLine("Noto'g'ri format!");
            }
            
            Console.WriteLine("\nAsosiy menyuga qaytish uchun Enter tugmasini bosing...");
            Console.ReadLine();
        }

        public static void BorrowBook(ref List<Book> books, ref Reader currentReader, string booksFilePath, string readersFilePath, List<Reader> readers)
        {
            Console.Clear();
            Console.WriteLine("========= KITOB OLISH =========");
            
            var availableBooks = books.Where(b => b.AvailableQuantity > 0).ToList();
            
            if (availableBooks.Count == 0)
            {
                Console.WriteLine("Kutubxonada mavjud kitoblar yo'q.");
                Console.WriteLine("Asosiy menyuga qaytish uchun Enter tugmasini bosing...");
                Console.ReadLine();
                return;
            }
            
            Console.WriteLine("Mavjud kitoblar:");
            foreach (var book in availableBooks)
            {
                Console.WriteLine($"{book.Id}. {book.Title} - {book.Author} ({book.AvailableQuantity}/{book.TotalQuantity})");
            }
            
            Console.Write("\nOlmoqchi bo'lgan kitob raqamini kiriting: ");
            if (int.TryParse(Console.ReadLine(), out int bookId))
            {
                Book book = books.FirstOrDefault(b => b.Id == bookId);
                if (book != null)
                {
                    if (book.AvailableQuantity > 0)
                    {
                        if (currentReader.BorrowedBooks.Contains(book.Id))
                        {
                            Console.WriteLine("Siz bu kitobni allaqachon olgansiz!");
                        }
                        else
                        {
                            book.AvailableQuantity--;
                            book.Readers.Add($"{currentReader.FirstName} {currentReader.LastName}");
                            
                            currentReader.BorrowedBooksCount++;
                            currentReader.BorrowedBooks.Add(book.Id);
                            
                            int readerId = currentReader.Id; 
                            int index = readers.FindIndex(r => r.Id == readerId); 
                            
                            if (index != -1)
                            {
                                readers[index] = currentReader;
                            }
                            
                            DataManager.SaveData(books, readers, booksFilePath, readersFilePath);
                            
                            Console.WriteLine($"\"{book.Title}\" kitobini muvaffaqiyatli oldingiz!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Bu kitob hozirda mavjud emas!");
                    }
                }
                else
                {
                    Console.WriteLine("Bunday raqamli kitob topilmadi!");
                }
            }
            else
            {
                Console.WriteLine("Noto'g'ri format!");
            }
            
            Console.WriteLine("\nAsosiy menyuga qaytish uchun Enter tugmasini bosing...");
            Console.ReadLine();
        }

        public static void ReturnBook(ref List<Book> books, ref Reader currentReader, string booksFilePath, string readersFilePath, List<Reader> readers)
        {
            Console.Clear();
            Console.WriteLine("========= KITOB QAYTARISH =========");
            
            if (currentReader.BorrowedBooksCount == 0)
            {
                Console.WriteLine("Sizda hozirda olingan kitoblar yo'q.");
                Console.WriteLine("Asosiy menyuga qaytish uchun Enter tugmasini bosing...");
                Console.ReadLine();
                return;
            }
            
            Console.WriteLine("Sizning kitoblaringiz:");
            foreach (int bookId in currentReader.BorrowedBooks)
            {
                Book book = books.FirstOrDefault(b => b.Id == bookId);
                if (book != null)
                {
                    Console.WriteLine($"{book.Id}. {book.Title} - {book.Author}");
                }
            }
            
            Console.Write("\nQaytarmoqchi bo'lgan kitob raqamini kiriting: ");
            if (int.TryParse(Console.ReadLine(), out int returnBookId))
            {
                if (currentReader.BorrowedBooks.Contains(returnBookId))
                {
                    Book book = books.FirstOrDefault(b => b.Id == returnBookId);
                    if (book != null)
                    {
                        book.AvailableQuantity++;
                        book.Readers.Remove($"{currentReader.FirstName} {currentReader.LastName}");
                        
                        currentReader.BorrowedBooksCount--;
                        currentReader.BorrowedBooks.Remove(returnBookId);
                        
                        int readerId = currentReader.Id; 
                        int index = readers.FindIndex(r => r.Id == readerId); 
                        if (index != -1)
                        {
                            readers[index] = currentReader;
                        }
                        
                        DataManager.SaveData(books, readers, booksFilePath, readersFilePath);
                        
                        Console.WriteLine($"\"{book.Title}\" kitobini muvaffaqiyatli qaytardingiz!");
                    }
                }
                else
                {
                    Console.WriteLine("Siz bu kitobni olmagan edingiz!");
                }
            }
            else
            {
                Console.WriteLine("Noto'g'ri format!");
            }
            
            Console.WriteLine("\nAsosiy menyuga qaytish uchun Enter tugmasini bosing...");
            Console.ReadLine();
        }

        public static void ShowMyBooks(List<Book> books, Reader currentReader)
        {
            Console.Clear();
            Console.WriteLine("========= MENING KITOBLARIM =========");
            
            if (currentReader.BorrowedBooksCount == 0)
            {
                Console.WriteLine("Sizda hozirda olingan kitoblar yo'q.");
            }
            else
            {
                Console.WriteLine($"Siz jami {currentReader.BorrowedBooksCount} ta kitob olgansiz:");
                
                foreach (int bookId in currentReader.BorrowedBooks)
                {
                    Book book = books.FirstOrDefault(b => b.Id == bookId);
                    if (book != null)
                    {
                        Console.WriteLine($"{book.Id}. {book.Title} - {book.Author}");
                    }
                }
            }
            
            Console.WriteLine("\nAsosiy menyuga qaytish uchun Enter tugmasini bosing...");
            Console.ReadLine();
        }
    }
}









