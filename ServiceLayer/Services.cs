﻿using DAL.Controllers;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLayer
{
    public class Services
    {
        Response response;
        Validation validation;
        public Services()
        {
            response = new Response();
            validation = new Validation();
        }
        public Response GetById(int id, BookRepository bookRepository)
        {
            if (validation.IsIdNegative(id))
            {
                response.StatusCode = 400;
                response.ErrorMessages.Add("Invalid Id, Id should be a positive number.");
            }
            else
            {
                var books = bookRepository.GetAllBooks();
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

        public Response GetAllBooks(BookRepository bookRepository)
        {
            var books = bookRepository.GetAllBooks();
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

        public Response PostByBook(Book book, BookRepository bookRepository)
        {
            Validate(book);
            if (response.ErrorMessages.Count == 0)
            {
                bookRepository.AddBook(book);
                response.StatusCode = 200;
                response.Result = bookRepository.GetAllBooks();
            }
            return response;
        }

        public Response UpdateBook(Book book, BookRepository bookRepository)
        {
            Validate(book);
            var isBookUpdated = bookRepository.UpdateBook(book);
            if (isBookUpdated && response.ErrorMessages.Count == 0)
            {
                response.StatusCode = 200;
                response.Result = bookRepository.GetAllBooks();
                
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
    }
}
