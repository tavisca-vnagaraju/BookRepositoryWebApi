using DAL.Controllers;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLayer
{
    public class Services
    {
        Response response;
        public Services()
        {
            response = new Response();
        }
        public Response GetById(int id, BookRepository bookRepository)
        {
            if (id < 0)
            {
                response.StatusCode = 400;
                response.ErrorMessage = "Invalid Id, Id should be a positive number.";
                return response;
            }
            var books = bookRepository.GetAllBooks();
            var book = books.Find(x => x.Id == id);
            if (book == null)
            {
                response.StatusCode = 404;
                response.ErrorMessage = "Book Not Found";
                return response;
            }
            else
            {
                response.StatusCode = 200;
                response.Result = book;
            }
            return response;
        }

        public Response GetAllBooks(BookRepository bookRepository)
        {
            var books = bookRepository.GetAllBooks();
            if(books.Count <=0)
            {
                response.StatusCode = 400;
                response.ErrorMessage = "Books Not Found";
                return response;
            }
            else
            {
                response.StatusCode = 200;
                response.Result = books;
                return response;
            }
        }

        public Response PostByBook(Book book, BookRepository bookRepository)
        {
            if (book.Id < 0)
            {
                response.StatusCode = 400;
                response.ErrorMessage = "Invalid Id, Id should be a positive number.";
                return response;
            }
            if (book.Price < 0)
            {
                response.StatusCode = 400;
                response.ErrorMessage = "Invalid Price, Price should be a positive number.";
                return response;
            }
            if (!book.Name.All(x => char.IsLetter(x) || x == ' '))
            {
                response.StatusCode = 400;
                response.ErrorMessage = "Book Name Should Have only Characters";
                return response;
            }
            if (!book.Category.All(x => char.IsLetter(x) || x == ' '))
            {
                response.StatusCode = 400;
                response.ErrorMessage = "Category Name Should Have only Characters";
                return response;
            }
            if (!book.Author.All(x => char.IsLetter(x) || x == ' ' || x == '.'))
            {
                response.StatusCode = 400;
                response.ErrorMessage = "Author Name Should Have only Characters";
                return response;
            }
            bookRepository.AddBook(book);
            response.StatusCode = 200;
            response.Result = bookRepository.GetAllBooks();
            return response;
        }

        public Response UpdateBook(Book book, BookRepository bookRepository)
        {
            if (book.Id < 0)
            {
                response.StatusCode = 400;
                response.ErrorMessage = "Invalid Id, Id should be a positive number.";
                return response;
            }
            if (book.Price < 0)
            {
                response.StatusCode = 400;
                response.ErrorMessage = "Invalid Price, Price should be a positive number.";
                return response;
            }
            if (!book.Name.All(x => char.IsLetter(x) || x == ' '))
            {
                response.StatusCode = 400;
                response.ErrorMessage = "Book Name Should Have only Characters";
                return response;
            }
            if (!book.Category.All(x => char.IsLetter(x) || x == ' '))
            {
                response.StatusCode = 400;
                response.ErrorMessage = "Category Name Should Have only Characters";
                return response;
            }
            if (!book.Author.All(x => char.IsLetter(x) || x == ' ' || x == '.'))
            {
                response.StatusCode = 400;
                response.ErrorMessage = "Author Name Should Have only Characters";
                return response;
            }
            if (bookRepository.UpdateBook(book))
            {
                response.StatusCode = 200;
                response.Result = bookRepository.GetAllBooks();
                return response;
            }
            else
            {
                response.StatusCode = 404;
                response.ErrorMessage = "Book Not Found";
            }
            return response;
        }
    }
}
