using Book.DataAccess;
using Book.DataAccess.Models;
using Book.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Services.Services
{
    public class BooksService
    {
        private readonly AppDbContext _context;

        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Mybooks>> GetAllAsync() => await _context.MyBooks.ToListAsync();

        public async Task<Mybooks?> GetByIdAsync(int id) => await _context.MyBooks.FindAsync(id);

        public async Task<Mybooks> AddAsync(Mybooks book)
        {
            _context.MyBooks.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Mybooks?> UpdateAsync(int id, Mybooks updatedBook)
        {
            var book = await _context.MyBooks.FindAsync(id);
            if (book == null) return null;

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Genre = updatedBook.Genre;

            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _context.MyBooks.FindAsync(id);
            if (book == null) return false;

            _context.MyBooks.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Mybooks>> FilterByGenreAsync(string genre) =>
            await _context.MyBooks.Where(b => b.Genre == genre).ToListAsync();
    }
}
