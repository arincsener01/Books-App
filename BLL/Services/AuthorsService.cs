using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IAuthorsService
    {
        public IQueryable<AuthorsModel> Query();
        public ServiceBase Create(Author record);
        public ServiceBase Update(Author record);
        public ServiceBase Delete(int id);

    }
    public class AuthorsService : ServiceBase, IAuthorsService
    {
        public AuthorsService(Db db) : base(db)
        {
        }
        public IQueryable<AuthorsModel> Query()
        {
            return _db.Authors
              .OrderBy(x => x.Name)
              .Select(x => new AuthorsModel
              {
                  Record = x
              });
        }

        public ServiceBase Create(Author record)
        {
            if (record == null)
                return Error("Author record cannot be null.");

            if (string.IsNullOrWhiteSpace(record.Name) || string.IsNullOrWhiteSpace(record.Surname))
                return Error("Author name and surname are required.");

            record.Name = record.Name?.Trim();
            record.Surname = record.Surname?.Trim();

            if (_db.Authors.Any(x => x.Name.ToUpper() == record.Name.ToUpper() && x.Surname.ToUpper() == record.Surname.ToUpper()))
                return Error("This author already exists.");

            _db.Authors.Add(record);
            _db.SaveChanges();
            return Success("Author added successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Authors.Include(x => x.Books).SingleOrDefault(x => x.Id == id);
            if (entity == null)
                return Error("Author not found.");
            if (entity.Books.Any())
                return Error("This author has associated books. Remove or reassign books before deleting the author.");
            _db.Authors.Remove(entity);
            _db.SaveChanges();
            return Success("Author deleted successfully.");
        }

        public ServiceBase Update(Author record)
        {
            if (record == null)
                return Error("Author record cannot be null.");

            if (string.IsNullOrWhiteSpace(record.Name) || string.IsNullOrWhiteSpace(record.Surname))
                return Error("Author name and surname are required.");

            record.Name = record.Name?.Trim();
            record.Surname = record.Surname?.Trim();

            if (_db.Authors.Any(x => x.Name.ToUpper() == record.Name.ToUpper() &&
                                     x.Surname.ToUpper() == record.Surname.ToUpper() &&
                                     x.Id != record.Id))
                return Error("Another author with the same name and surname already exists.");

            var entity = _db.Authors.SingleOrDefault(x => x.Id == record.Id);
            if (entity == null)
                return Error("Author not found.");

            entity.Name = record.Name;
            entity.Surname = record.Surname;

            _db.Authors.Update(entity);
            _db.SaveChanges();
            return Success("Author updated successfully.");
        }
    }
}
