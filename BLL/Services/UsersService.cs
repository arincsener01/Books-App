//using BLL.DAL;
//using BLL.Models;
//using BLL.Services.Bases;
//using Microsoft.AspNetCore.Mvc.Controllers;
//using Microsoft.EntityFrameworkCore;

//namespace BLL.Services
//{
//    public interface IUsersService
//    {
//        public IQueryable<UsersModel> Query();
//        public ServiceBase Create(User record);
//        public ServiceBase Update(User record);
//        public ServiceBase Delete(int id);
//    }
//    public class UsersService : ServiceBase, IUsersService
//    {
//        public UsersService(Db db) : base(db)
//        {
//        }
//        public IQueryable<UsersModel> Query()
//        {
//            return _db.Users
//                      .Include(u => u.Role) // Eagerly load the Role navigation property
//                      .OrderBy(u => u.UserName)
//                      .Select(u => new UsersModel
//                      {
//                          Record = u
//                      });
//        }
//        public ServiceBase Create(User record)
//        {
//            if (record == null)
//                return Error("User record cannot be null.");

//            if (string.IsNullOrWhiteSpace(record.UserName))
//                return Error("User name is required.");

//            if (string.IsNullOrWhiteSpace(record.Password))
//                return Error("Password is required.");

//            record.UserName = record.UserName.Trim();
//            record.Password = record.Password.Trim();

//            if (_db.Users.Any(u => u.UserName.ToUpper() == record.UserName.ToUpper()))
//                return Error("A user with this name already exists.");

//            _db.Users.Add(record);
//            _db.SaveChanges();
//            return Success("User created successfully.");
//        }

//        public ServiceBase Delete(int id)
//        {
//            var entity = _db.Users.Include(u => u.Role).SingleOrDefault(u => u.Id == id);
//            if (entity == null)
//                return Error("User not found.");

//            if (entity.IsActive)
//                return Error("Cannot delete an active user. Deactivate the user before deletion.");

//            _db.Users.Remove(entity);
//            _db.SaveChanges();
//            return Success("User deleted successfully.");
//        }

//        public ServiceBase Update(User record)
//        {
//            if (record == null)
//                return Error("User record cannot be null.");

//            if (string.IsNullOrWhiteSpace(record.UserName))
//                return Error("User name is required.");

//            record.UserName = record.UserName.Trim();

//            if (_db.Users.Any(u => u.UserName.ToUpper() == record.UserName.ToUpper() && u.Id != record.Id))
//                return Error("Another user with the same name already exists.");

//            var entity = _db.Users.SingleOrDefault(u => u.Id == record.Id);
//            if (entity == null)
//                return Error("User not found.");

//            // Update fields
//            entity.UserName = record.UserName;
//            entity.Password = record.Password;
//            entity.IsActive = record.IsActive;
//            entity.RoleId = record.RoleId;

//            _db.Users.Update(entity);
//            _db.SaveChanges();
//            return Success("User updated successfully.");
//        }
//    }
//}
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class UserService : ServiceBase, IService<User, UsersModel>
    {
        public UserService(Db db) : base(db)
        {
        }

        public ServiceBase Create(User record)
        {
            throw new NotImplementedException();
        }

        public ServiceBase Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UsersModel> Query()
        {
            return _db.Users.Include(u => u.Role).OrderByDescending(u => u.IsActive).ThenBy(u => u.UserName).Select(u => new UsersModel()
            {
                Record = u
            });
        }

        public ServiceBase Update(User record)
        {
            throw new NotImplementedException();
        }
    }
}