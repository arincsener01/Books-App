using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IGenresService
    {
        public IQueryable<GenresModel> Query();
        public ServiceBase Create(Genre record);
        public ServiceBase Update(Genre record);
        public ServiceBase Delete(int id);
    }

    public class GenresService : ServiceBase, IGenresService
    {
        public GenresService(Db db) : base(db)
        {
        }
        public IQueryable<GenresModel> Query()
        {
            return _db.Genres.OrderBy(x => x.Name).Select(x => new GenresModel() { Record = x });
        }

        public ServiceBase Create(Genre record)
        {
            if (_db.Genres.Any(x => x.Name.ToUpper() == record.Name.ToUpper()))
                return Error("This genre already exists.");
            record.Name = record.Name?.Trim();
            _db.Genres.Add(record);
            _db.SaveChanges();
            return Success("Genre added successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Genres.Include(x => x.BookGenres).SingleOrDefault(x => x.Id == id);
            if (entity == null)
                return Error("Genre not found.");
            if (entity.BookGenres.Any())
                return Error("This genre has associated bookgenres. Remove or reassign books before deleting the genre.");
            _db.Genres.Remove(entity);
            _db.SaveChanges();
            return Success("Genre deleted successfully.");
        }

        public ServiceBase Update(Genre record)
        {
            if (_db.Genres.Any(x => x.Name.ToUpper() == record.Name.ToUpper() && x.Id != record.Id))
                return Error("This genre already exists.");
            var entity = _db.Genres.SingleOrDefault(x => x.Id == record.Id);
            if (entity == null)
                return Error("Genre not found.");
            entity.Name = record.Name?.Trim();
            _db.Genres.Update(entity);
            _db.SaveChanges();
            return Success("Genre updated successfully.");
        }
    }
}
