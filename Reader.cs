
using System;
using System.Collections.Generic;

namespace LibraryManagementSystem
{
    public class Reader
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int BorrowedBooksCount { get; set; }
        public List<int> BorrowedBooks { get; set; }
    }
}








