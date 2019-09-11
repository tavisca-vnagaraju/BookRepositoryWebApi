using DAL.Controllers;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ServiceLayer
{
    public class Services
    {
        Response response;
        Validation validation;
        BookValidator validator;
        private IBookRepository _bookRepository;
        public Services(IBookRepository bookRepository)
        {
            response = new Response();
            validation = new Validation();
            validator = new BookValidator();
            _bookRepository = bookRepository;
        }
        public Response GetById(int id)
        {
            if (validation.IsIdNegative(id))
            {
                response.StatusCode = 400;
                response.ErrorMessages.Add("Invalid Id, Id should be a positive number.");
            }
            else
            {
                var books = _bookRepository.GetAllBooks();
                var book = books.Find(x => x.Id == id);
                if (book == null)
                {
                    response.StatusCode = 404;
                    response.ErrorMessages.Add("Book Not Found");
                }
                else
                {
                    response.StatusCode = 200;
                    response.Result = book;
                }
            }
            return response;
        }

        public Response GetAllBooks()
        {
            var books = _bookRepository.GetAllBooks();
            if(books.Count <=0)
            {
                response.StatusCode = 400;
                response.ErrorMessages.Add("Books Not Found");
            }
            else
            {
                response.StatusCode = 200;
                response.Result = books;
            }
            return response;
        }

        public Response PostByBook(Book book)
        {
            //Validate(book);
            FluentValidate(book);
            if (response.ErrorMessages.Count == 0)
            {
                _bookRepository.AddBook(book);
                response.StatusCode = 200;
                response.Result = _bookRepository.GetAllBooks();
            }
            return response;
        }

        public Response UpdateBook(Book book)
        {
            //Validate(book);
            FluentValidate(book);
            var isBookUpdated = _bookRepository.UpdateBook(book);
            if (isBookUpdated && response.ErrorMessages.Count == 0)
            {
                response.StatusCode = 200;
                response.Result = _bookRepository.GetAllBooks();
                
            }
            else if(response.ErrorMessages.Count == 0)
            {
                response.StatusCode = 404;
            }
            return response;
        }
        private void Validate(Book book)
        {
            if (validation.IsIdNegative(book.Id))
            {
                response.StatusCode = 400;
                response.ErrorMessages.Add("Invalid Id, Id should be a positive number.");
            }
            if (validation.IsPriceNegative(book.Price))
            {
                response.StatusCode = 400;
                response.ErrorMessages.Add("Invalid Price, Price should be a positive number.");
            }
            if (!validation.ContainsCharacters(book.Name))
            {
                response.StatusCode = 400;
                response.ErrorMessages.Add("Book Name Should Have only Characters");
            }
            if (!validation.ContainsCharacters(book.Category))
            {
                response.StatusCode = 400;
                response.ErrorMessages.Add("Category Name Should Have only Characters");
            }
            if (!validation.ContainsCharacters(book.Author))
            {
                response.StatusCode = 400;
                response.ErrorMessages.Add("Author Name Should Have only Characters");
            }
        }
        private void FluentValidate(Book book)
        {
            var result = validator.Validate(book);
            if (!result.IsValid)
            {
                foreach (var item in result.Errors)
                {
                    response.ErrorMessages.Add(item.ErrorMessage);
                }
                response.StatusCode = 400;
            }
        }

        public Response DeleteBookById(int id)
        {
            if (validation.IsIdNegative(id))
            {
                response.StatusCode = 400;
                response.ErrorMessages.Add("Invalid Id, Id should be a positive number.");
            }
            else
            {
                var isBookExists = _bookRepository.IsBookExists(id);
                if (isBookExists)
                {
                    var IsBookDeleted = _bookRepository.DeleteBookById(id);
                    if (IsBookDeleted)
                    {
                        response.StatusCode = 200;
                        response.Result = _bookRepository.GetAllBooks();
                    }
                }
                else
                {
                    response.StatusCode = 404;
                    response.ErrorMessages.Add("Book Not Found");
                }
            }
            return response;
        }
    }
}