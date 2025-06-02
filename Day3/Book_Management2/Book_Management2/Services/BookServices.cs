using Book_Management.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
namespace Book_Management.Services;

public class BookServices
{
    private static List<Book> _books = new List<Book>();
    private static int _nextId = 1;

    public List<Book> GetAll() => _books;

    public Book? GetById(int id) => _books.FirstOrDefault(b => b.Id == id);

    public Book Add(Book book)
    {
        book.Id = _nextId++;
        _books.Add(book);
        return book;
    }

    public bool Update(int id, Book updatedBook)
    {
        var existingBook = GetById(id);
        if (existingBook == null) return false;

        existingBook.Title = updatedBook.Title;
        existingBook.Author = updatedBook.Author;
        existingBook.Year = updatedBook.Year;
        return true;
    }

    public bool Delete(int id)
    {
        var book = GetById(id);
        if (book == null) return false;

        _books.Remove(book);
        return true;
    }
}

