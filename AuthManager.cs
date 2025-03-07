

using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem
{
    public static class AuthManager
    {
        public static void ShowLoginMenu(ref List<Reader> readers, ref Reader currentReader, string booksFilePath, string readersFilePath)
        {
            Console.Clear();
            Console.WriteLine("========= KUTUBXONA TIZIMI =========");
            Console.WriteLine("1. Tizimga kirish (Login)");
            Console.WriteLine("2. Ro'yxatdan o'tish (Registration)");
            Console.WriteLine("3. Dasturdan chiqish");
            Console.WriteLine("====================================");
            Console.Write("Tanlovingizni kiriting: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        Login(readers, ref currentReader);
                        break;
                    case 2:
                        Register(ref readers, ref currentReader, booksFilePath, readersFilePath);
                        break;
                    case 3:
                        Environment.Exit(0);
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

        public static void Login(List<Reader> readers, ref Reader currentReader)
        {
            Console.Clear();
            Console.WriteLine("========= TIZIMGA KIRISH =========");
            Console.Write("Login: ");
            string login = Console.ReadLine();
            Console.Write("Parol: ");
            string password = Console.ReadLine();

            Reader reader = readers.FirstOrDefault(r => r.Login == login && r.Password == password);
            if (reader != null)
            {
                currentReader = reader;
                Console.WriteLine($"Xush kelibsiz, {reader.FirstName} {reader.LastName}!");
                Console.WriteLine("Davom etish uchun Enter tugmasini bosing...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Login yoki parol noto'g'ri! Davom etish uchun Enter tugmasini bosing...");
                Console.ReadLine();
            }
        }

        public static void Register(ref List<Reader> readers, ref Reader currentReader, string booksFilePath, string readersFilePath)
        {
            Console.Clear();
            Console.WriteLine("========= RO'YXATDAN O'TISH =========");
            Console.Write("Login (kamida 4 ta belgi): ");
            string login = Console.ReadLine();

            if (readers.Any(r => r.Login == login))
            {
                Console.WriteLine("Bu login band! Boshqa login tanlang.");
                Console.WriteLine("Davom etish uchun Enter tugmasini bosing...");
                Console.ReadLine();
                return;
            }

            if (login.Length < 4)
            {
                Console.WriteLine("Login kamida 4 ta belgidan iborat bo'lishi kerak!");
                Console.WriteLine("Davom etish uchun Enter tugmasini bosing...");
                Console.ReadLine();
                return;
            }

            Console.Write("Parol (kamida 6 ta belgi): ");
            string password = Console.ReadLine();

            if (password.Length < 6)
            {
                Console.WriteLine("Parol kamida 6 ta belgidan iborat bo'lishi kerak!");
                Console.WriteLine("Davom etish uchun Enter tugmasini bosing...");
                Console.ReadLine();
                return;
            }

            Console.Write("Ismingiz: ");
            string firstName = Console.ReadLine();

            Console.Write("Familiyangiz: ");
            string lastName = Console.ReadLine();

            Console.Write("Yoshingiz: ");
            if (!int.TryParse(Console.ReadLine(), out int age) || age <= 0)
            {
                Console.WriteLine("Yoshingiz noto'g'ri formatda kiritildi!");
                Console.WriteLine("Davom etish uchun Enter tugmasini bosing...");
                Console.ReadLine();
                return;
            }

            int newId = readers.Count > 0 ? readers.Max(r => r.Id) + 1 : 1;

            Reader newReader = new Reader
            {
                Id = newId,
                Login = login,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                BorrowedBooksCount = 0,
                BorrowedBooks = new List<int>()
            };

            readers.Add(newReader);
            DataManager.SaveData(new List<Book>(), readers, booksFilePath, readersFilePath);

            Console.WriteLine("Ro'yxatdan muvaffaqiyatli o'tdingiz!");
            Console.WriteLine("Davom etish uchun Enter tugmasini bosing...");
            Console.ReadLine();

            currentReader = newReader;
        }
    }
}









