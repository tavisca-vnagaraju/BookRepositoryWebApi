using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public string Author { get; set; }
    }
}
