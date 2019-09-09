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

        public bool UpdateBook(Book book)
        {
            var _book = _bookList.Find(x => x.Id == book.Id);
            if(_book != null)
            {
                var index = _bookList.IndexOf(_book);
                _bookList[index] = book;
            }
            if(_book == null)
            {
                return false;
            }
            return true;
        }

        public bool IsBookExists(int id)
        {
            var book = _bookList.Find(x => x.Id == id);
            if(book == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool DeleteBookById(int id)
        {
            var book = _bookList.Find(x => x.Id == id);
            if (book != null)
            {
                var index = _bookList.IndexOf(book);
                _bookList.RemoveAt(index);
            }
            if (book == null)
            {
                return false;
            }
            return true;
        }
    }

}
