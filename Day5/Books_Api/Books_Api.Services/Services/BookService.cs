using Books_Api.Data.Models;
using Books_Api.Entities.Entities;
using Books_Api.Entities.Repositories.Interface;
using Books_Api.Services.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Api.Services.Services
{
    public class BookService : IBookService
    {
        private List<Book> _books;
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            _books = new List<Book>();
        }

        // To Add Book
        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        // To Get All Books
        public async Task<List<BookDetails>> GetAll()
        {
            return await _bookRepository.GetAll();
        }

        // To Get Single Book
        public Book? GetBookById(int id)
        {
            return _books.Find(x => x.Id == id);
        }

        public async Task InsertBook(BookDetails bookDetails)
        {
            await _bookRepository.InsertBook(bookDetails);
        }


        public BookDetails GetBookDetailsById(int id)
        {
            return _bookRepository.GetById(id);
        }
    }
}
