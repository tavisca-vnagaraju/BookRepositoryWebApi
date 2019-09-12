using DAL.Model;
using System.Collections.Generic;

namespace DAL.Controllers
{
    public interface IBookRepository
    {
        void AddBook(Book book);
        List<Book> GetAllBooks();
        bool UpdateBook(Book book);
        bool IsBookExists(int id);
        bool DeleteBookById(int id);
        Book GetBookById(int id);
    }
}
