using DAL.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DAL.Controllers
{
    public class BookRepository
    {
        private List<Book> _bookList = new List<Book>();
        
        public void AddBook(Book book)
        {
            var _book = _bookList.Find(x => x.Id == book.Id);
            if(_book == null)
            {
                _bookList.Add(book);
            }
        }
        public List<Book> GetAllBooks()
        {
            return _bookList;
        }

        public List<Book> UpdateBook(int id, Book book)
        {
            var _book = _bookList.Find(x => x.Id == id);
            if(_book != null)
            {
                _bookList.Remove(_book);
                AddBook(book);
            }
            return _bookList;
        }
    }

}
