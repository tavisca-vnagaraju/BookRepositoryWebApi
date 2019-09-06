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
        public void CheckForPostByBookNegativeId()
        {
            BookRepository bookRepository = new BookRepository();
            Book book = new Book { Name = "Harry Potter", Category="Fiction",Author = "Rolling", Price = 400, Id = -1 };
            Services services = new Services();
            var result = services.PostByBook(book, bookRepository);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public void CheckForPostByBookNegativePrice()
        {
            BookRepository bookRepository = new BookRepository();
            Book book = new Book { Name = "Harry Potter",Category="Fiction" ,Author = "Rolling", Price = -400, Id = 1 };
            Services services = new Services();
            var result = services.PostByBook(book, bookRepository);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public void CheckForPostByBookInvalidName()
        {
            BookRepository bookRepository = new BookRepository();
            Book book = new Book { Name = "Harry Potter123",Category="Fiction" ,Author = "Rolling", Price = 400, Id = 1 };
            Services services = new Services();
            var result = services.PostByBook(book, bookRepository);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public void CheckForPostByBookInvalidAuthor()
        {
            BookRepository bookRepository = new BookRepository();
            Book book = new Book { Name = "Harry Potter", Category = "Fiction", Author = "Rolling123", Price = 400, Id = 1 };
            Services services = new Services();
            var result = services.PostByBook(book, bookRepository);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public void CheckForPostByBookInvalidCategory()
        {
            BookRepository bookRepository = new BookRepository();
            Book book = new Book { Name = "Harry Potter", Category = "Fiction123", Author = "Rolling123", Price = 400, Id = 1 };
            Services services = new Services();
            var result = services.PostByBook(book, bookRepository);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public void CheckForUpdatingBookSuccess()
        {
            BookRepository bookRepository = new BookRepository();
            Book book = new Book { Name = "Harry Potter", Category = "Fiction", Author = "Rolling", Price = 400, Id = 1 };
            bookRepository.AddBook(book);
            Services services = new Services();
            Book bookUpdate = new Book { Name = "Harry Potter Globet of Fire", Category = "Fiction", Author = "Rolling", Price = 400, Id = 1 };
            var result = services.UpdateBook(bookUpdate, bookRepository);
            Assert.Equal(200,result.StatusCode);
        }
        [Fact]
        public void CheckForUpdateBookWhichDoesNotExists()
        {
            BookRepository bookRepository = new BookRepository();
            Services services = new Services();
            Book bookUpdate = new Book { Name = "Harry Potter Globet of Fire", Category = "Fiction", Author = "Rolling", Price = 400, Id = 1 };
            var result = services.UpdateBook(bookUpdate, bookRepository);
            Assert.Equal(404, result.StatusCode);
        }
        [Fact]
        public void CheckForUpdateBookWithNegativeId()
        {
            BookRepository bookRepository = new BookRepository();
            Services services = new Services();
            Book bookUpdate = new Book { Name = "Harry Potter Globet of Fire", Category = "Fiction", Author = "Rolling", Price = 400, Id = -1 };
            var result = services.UpdateBook(bookUpdate, bookRepository);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public void CheckForUpdateBookWithInvalidName()
        {
            BookRepository bookRepository = new BookRepository();
            Services services = new Services();
            Book bookUpdate = new Book { Name = "Harry Potter Globet of Fire123", Category = "Fiction", Author = "Rolling", Price = 400, Id = 1 };
            var result = services.UpdateBook(bookUpdate, bookRepository);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public void CheckForUpdateBookWithInvalidCategory()
        {
            BookRepository bookRepository = new BookRepository();
            Services services = new Services();
            Book bookUpdate = new Book { Name = "Harry Potter Globet of Fire", Category = "Fiction123", Author = "Rolling", Price = 400, Id = 1 };
            var result = services.UpdateBook(bookUpdate, bookRepository);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public void CheckForUpdateBookWithInvalidAuthor()
        {
            BookRepository bookRepository = new BookRepository();
            Services services = new Services();
            Book bookUpdate = new Book { Name = "Harry Potter Globet of Fire", Category = "Fiction", Author = "Rolling123", Price = 400, Id = 1 };
            var result = services.UpdateBook(bookUpdate, bookRepository);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public void CheckForUpdateBookWithInvalidPrice()
        {
            BookRepository bookRepository = new BookRepository();
            Services services = new Services();
            Book bookUpdate = new Book { Name = "Harry Potter Globet of Fire", Category = "Fiction", Author = "Rolling", Price = -400, Id = 1 };
            var result = services.UpdateBook(bookUpdate, bookRepository);
            Assert.Equal(400, result.StatusCode);
        }
    }
}
