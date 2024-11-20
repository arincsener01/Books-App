using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IBooksService
    {
        public IQueryable<BooksModel> Query();
        public ServiceBase Create(Book record);
        public ServiceBase Update(Book record);
        public ServiceBase Delete(int id);
    }
    public class BooksService : ServiceBase, IBooksService
    {
        public BooksService(Db db) : base(db)
        {
        }
        public IQueryable<BooksModel> Query()
        {
            return _db.Books
                      .Include(b => b.Author) 
                      .Include(b => b.BookGenres) 
                      .ThenInclude(bg => bg.Genre) 
                      .OrderBy(x => x.Name)
                      .Select(x => new BooksModel()
                      {
                          Record = x
                      });
        }
        public ServiceBase Create(Book record)
        {
            if (record == null)
                return Error("Book record cannot be null.");

            if (string.IsNullOrWhiteSpace(record.Name))
                return Error("Book name is required.");

            record.Name = record.Name?.Trim();

            if (_db.Books.Any(x => x.Name.ToUpper() == record.Name.ToUpper()))
                return Error("A book with this name already exists.");

            _db.Books.Add(record);
            _db.SaveChanges();
            return Success("Book added successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Books.Include(b => b.BookGenres).SingleOrDefault(x => x.Id == id);
            if (entity == null)
                return Error("Book not found.");

            if (entity.BookGenres.Any())
                return Error("This book has associated genres and cannot be deleted.");

            _db.Books.Remove(entity);
            _db.SaveChanges();
            return Success("Book deleted successfully.");
        }

        public ServiceBase Update(Book record)
        {
            if (record == null)
                return Error("Book record cannot be null.");

            if (string.IsNullOrWhiteSpace(record.Name))
                return Error("Book name is required.");

            record.Name = record.Name.Trim();

            if (_db.Books.Any(x => x.Name.ToUpper() == record.Name.ToUpper() && x.Id != record.Id))
                return Error("Another book with the same name already exists.");

            var entity = _db.Books.SingleOrDefault(x => x.Id == record.Id);
            if (entity == null)
                return Error("Book not found.");

            entity.Name = record.Name;
            entity.NumberOfPages = record.NumberOfPages;
            entity.PublishDate = record.PublishDate;
            entity.Price = record.Price;
            entity.IsTopSeller = record.IsTopSeller;
            entity.AuthorId = record.AuthorId;

            _db.Books.Update(entity);
            _db.SaveChanges();
            return Success("Book updated successfully.");
        }
    }
}
