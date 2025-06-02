using Books_Api.Entities.Entities;
using Books_Api.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Api.Services.Services.Interface
{
    public interface IBookService
    {
        void AddBook(Book book);
        Task<List<BookDetails>> GetAll();
        Book? GetBookById(int id);
        Task InsertBook(BookDetails bookDetails);
        BookDetails GetBookDetailsById(int id);
    }   
}
