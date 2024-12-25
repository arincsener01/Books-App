// No need for this service either, it's just a wrapper for the Role class, and it's not used anywhere in the project
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IRolesService
    {
        public IQueryable<RolesModel> Query();
        public ServiceBase Create(Role record);
        public ServiceBase Update(Role record);
        public ServiceBase Delete(int id);
    }
    public class RolesService : ServiceBase, IRolesService
    {
        public RolesService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Role record)
        {
            if (_db.Roles.Any(x => x.Name.ToUpper() == record.Name.ToUpper()))
                return Error("This role already exists.");
            record.Name = record.Name?.Trim();
            _db.Roles.Add(record);
            _db.SaveChanges();
            return Success("Role added successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Roles.Include(x => x.Users).SingleOrDefault(x => x.Id == id);
            if (entity == null)
                return Error("Role not found.");
            if (entity.Users.Any())
                return Error("This Role has associated users. Remove or reassign users before deleting the role.");
            _db.Roles.Remove(entity);
            _db.SaveChanges();
            return Success("Role deleted successfully.");
        }

        public IQueryable<RolesModel> Query()
        {
            return _db.Roles.OrderBy(x => x.Name).Select(x => new RolesModel() { Record = x });
        }

        public ServiceBase Update(Role record)
        {
            if (_db.Roles.Any(x => x.Name.ToUpper() == record.Name.ToUpper() && x.Id != record.Id))
                return Error("This role already exists.");
            var entity = _db.Roles.SingleOrDefault(x => x.Id == record.Id);
            if (entity == null)
                return Error("Role not found.");
            entity.Name = record.Name?.Trim();
            _db.Roles.Update(entity);
            _db.SaveChanges();
            return Success("Role updated successfully.");
        }
    }
}
