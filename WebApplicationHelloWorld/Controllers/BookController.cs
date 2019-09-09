using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.Model;
using DAL.Controllers;
using ServiceLayer;

namespace WebApplicationHelloWorld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookRepository _bookRepository;
        Services services;
        private void LogToFile(string methodName, Response result)
        {
            FileLogger fileLogger = new FileLogger(DateTime.Now.ToString(), methodName , result.StatusCode, result.ErrorMessages);
            fileLogger.Log();
        }
        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            services = new Services();
        }
        // GET: api/Book
        [HttpGet]
        public Response Get()
        {   
            var result = services.GetAllBooks(_bookRepository);
            LogToFile("Get",result);
            return result;
            //return _bookRepository.GetAllBooks();
        }

        // GET: api/Book/5
        [HttpGet("{id}", Name = "Get")]
        public Response Get(int id)
        {
            var result = services.GetById(id, _bookRepository);
            HttpContext.Response.StatusCode = result.StatusCode;
            return result;
        }

        // POST: api/Book
        [HttpPost]
        public Response Post(Book book)
        {
            var result = services.PostByBook(book, _bookRepository);
            HttpContext.Response.StatusCode = result.StatusCode;
            LogToFile("Post",result);
            return result;
        }

        // PUT: api/Book/5
        [HttpPut]
        public Response Put([FromBody] Book book)
        {
            var result = services.UpdateBook(book, _bookRepository);
            HttpContext.Response.StatusCode = result.StatusCode;
            return result;
            //return _bookRepository.UpdateBook(id, book);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public Response Delete(int id)
        {
            var result = services.DeleteBookById(id, _bookRepository);
            HttpContext.Response.StatusCode = result.StatusCode;
            return result;
        }
    }
}