using DAL.Controllers;
using DAL.Model;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

namespace WebApiBookTests
{
    public class TestCasesForWebApi
    {
        [Fact]
        public void CheckForEmptyBooks()
        {
            BookRepository bookRepository = new BookRepository();
            Services services = new Services();
            var result = services.GetAllBooks(bookRepository);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("Books Not Found", result.ErrorMessage);
        }
        [Fact]
        public void CheckForAllBooks()
        {
            BookRepository bookRepository = new BookRepository();
            bookRepository.AddBook(new Book { Name = "Harry Potter", Author = "Rolling", Price = 400, Id = 1 });
            bookRepository.AddBook(new Book { Name = "You Can Win", Author = "Shiv Khera", Price = 200, Id = 2 });
            bookRepository.AddBook(new Book { Name = "Let us C", Author = "Yaswanth Kanetkar", Price = 300, Id = 3 });
            Services services = new Services();
            var result = services.GetAllBooks(bookRepository);
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void CheckForGetBookByIdPositive()
        {
            BookRepository bookRepository = new BookRepository();
            bookRepository.AddBook(new Book { Name = "Harry Potter", Author = "Rolling", Price = 400, Id = 1 });
            Services services = new Services();
            var result = services.GetById(1, bookRepository);
            var book = (Book)result.Result;
            Assert.Equal(200,result.StatusCode);
            Assert.Equal("Harry Potter", book.Name);
        }
        [Fact]
        public void CheckForGetBookByIdNegative()
        {
            BookRepository bookRepository = new BookRepository();
            Services services = new Services();
            var result = services.GetById(-1, bookRepository);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public void CheckForGetBookByIdForNull()
        {
            BookRepository bookRepository = new BookRepository();
            Services services = new Services();
            var result = services.GetById(1, bookRepository);
            Assert.Equal(404, result.StatusCode);
        }
        [Fact]
        public void CheckForGetBookPostByBookNegativeId()
        {
            BookRepository bookRepository = new BookRepository();
            Book book = new Book { Name = "Harry Potter", Author = "Rolling", Price = 400, Id = -1 };
            Services services = new Services();
            var result = services.PostByBook(book, bookRepository);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public void CheckForGetBookPostByBookNegativePrice()
        {
            BookRepository bookRepository = new BookRepository();
            Book book = new Book { Name = "Harry Potter", Author = "Rolling", Price = -400, Id = 1 };
            Services services = new Services();
            var result = services.PostByBook(book, bookRepository);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public void CheckForGetBookPostByBookInvalidName()
        {
            BookRepository bookRepository = new BookRepository();
            Book book = new Book { Name = "Harry Potter123",Category="Fiction" ,Author = "Rolling", Price = 400, Id = 1 };
            Services services = new Services();
            var result = services.PostByBook(book, bookRepository);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public void CheckForGetBookPostByBookInvalidAuthor()
        {
            BookRepository bookRepository = new BookRepository();
            Book book = new Book { Name = "Harry Potter", Category = "Fiction", Author = "Rolling123", Price = 400, Id = 1 };
            Services services = new Services();
            var result = services.PostByBook(book, bookRepository);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public void CheckForGetBookPostByBookInvalidCategory()
        {
            BookRepository bookRepository = new BookRepository();
            Book book = new Book { Name = "Harry Potter", Category = "Fiction123", Author = "Rolling123", Price = 400, Id = 1 };
            Services services = new Services();
            var result = services.PostByBook(book, bookRepository);
            Assert.Equal(400, result.StatusCode);
        }
    }
}
