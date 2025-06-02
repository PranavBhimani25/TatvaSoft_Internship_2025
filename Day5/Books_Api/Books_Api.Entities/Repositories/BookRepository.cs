using Books_Api.Entities.Context;
using Books_Api.Entities.Entities;
using Books_Api.Entities.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Api.Entities.Repositories
{
    public class BookRepository(BookDbContext bookDbContext) : IBookRepository
    {
        private readonly BookDbContext _dbContext = bookDbContext;

        public async Task InsertBook(BookDetails bookDetails)
        {
            await _dbContext.BookDetails.AddAsync(bookDetails);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<BookDetails>>  GetAll()
        {
            return await _dbContext.BookDetails.ToListAsync();
        }

        public BookDetails GetById(int id)
        {
            return _dbContext.BookDetails.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
