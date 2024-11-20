using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IBookGenresService
    {
        public IQueryable<BookGenresModel> Query();
        public ServiceBase Create(BookGenre record);
        public ServiceBase Update(BookGenre record);
        public ServiceBase Delete(int id);
        public ServiceBase Get(int id);
    }
    public class BookGenresService : ServiceBase, IBookGenresService
    {
        public BookGenresService(Db db) : base(db)
        {
        }

        public ServiceBase Create(BookGenre record)
        {
            if (record == null)
                return Error("The BookGenre record cannot be null.");

            var bookExists = _db.Books.Any(b => b.Id == record.BookId);
            if (!bookExists)
                return Error("The specified book does not exist.");

            var genreExists = _db.Genres.Any(g => g.Id == record.GenreId);
            if (!genreExists)
                return Error("The specified genre does not exist.");

            var relationshipExists = _db.BookGenres.Any(bg => bg.BookId == record.BookId && bg.GenreId == record.GenreId);
            if (relationshipExists)
                return Error("This book-genre relationship already exists.");

            _db.BookGenres.Add(record);
            _db.SaveChanges();

            return Success("Book-genre relationship created successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var record = _db.BookGenres.SingleOrDefault(bg => bg.Id == id);
            if (record == null)
                return Error("The specified book-genre relationship does not exist.");

            _db.BookGenres.Remove(record);
            _db.SaveChanges();

            return Success("Book-genre relationship deleted successfully.");
        }

        public ServiceBase Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<BookGenresModel> Query()
        {
            return _db.BookGenres
             .Include(bg => bg.Book)
             .Include(bg => bg.Genre)
             .Select(bg => new BookGenresModel
             {
                 Record = bg
             });
        }

        public ServiceBase Update(BookGenre record)
        {
            if (record == null)
                return Error("The BookGenre record cannot be null.");

            var existingRecord = _db.BookGenres.SingleOrDefault(bg => bg.Id == record.Id);
            if (existingRecord == null)
                return Error("The specified book-genre relationship does not exist.");

            var bookExists = _db.Books.Any(b => b.Id == record.BookId);
            if (!bookExists)
                return Error("The specified book does not exist.");

            var genreExists = _db.Genres.Any(g => g.Id == record.GenreId);
            if (!genreExists)
                return Error("The specified genre does not exist.");

            existingRecord.BookId = record.BookId;
            existingRecord.GenreId = record.GenreId;

            _db.BookGenres.Update(existingRecord);
            _db.SaveChanges();

            return Success("Book-genre relationship updated successfully.");
        }
    }
}
