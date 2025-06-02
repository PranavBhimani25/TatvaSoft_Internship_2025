using Books_Api.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Api.Entities.Repositories.Interface
{
    public interface IBookRepository
    {
        Task InsertBook(BookDetails bookDetails);
        Task<List<BookDetails>>  GetAll();
        BookDetails GetById(int id);
    }
}

