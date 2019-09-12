using DAL.Controllers;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ServiceStack.Redis;

namespace ServiceLayer
{
    public class Services
    {
        Response response;
        Validation validation;
        BookValidator validator;
        private IBookRepository _bookRepository;
        RedisManagerPool manager = new RedisManagerPool("localhost:6379");
        IRedisClient client;
        public Services(IBookRepository bookRepository)
        {
            response = new Response();
            validation = new Validation();
            validator = new BookValidator();
            _bookRepository = bookRepository;
            client = manager.GetClient();
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
                Book book;
                if (client.Get<Book>(id.ToString()) != null)
                {
                    book = client.Get<Book>(id.ToString());
                    response.IsResultFromCache = true;
                    response.StatusCode = 200;
                    response.Result = book;
                }
                else
                {
                    book = _bookRepository.GetBookById(id);
                    if (book == null)
                    {
                        response.StatusCode = 404;
                        response.ErrorMessages.Add("Book Not Found");
                    }
                    else
                    {
                        response.StatusCode = 200;
                        response.Result = book;
                        client.Set(book.Id.ToString(), book, DateTime.UtcNow.AddMinutes(1));
                    }
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
                client.Remove(book.Id.ToString());
                
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
                client.Remove(id.ToString());
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