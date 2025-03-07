
using System;
using System.Collections.Generic;

namespace LibraryManagementSystem
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int TotalQuantity { get; set; }
        public int AvailableQuantity { get; set; }
        public List<string> Readers { get; set; }
    }
}


