//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using BLL.Controllers.Bases;
//using BLL.Services;
//using BLL.Models;

//// Generated from Custom Template.

//namespace MVC.Controllers
//{
//    public class UsersController : MvcController
//    {
//        // Service injections:
//        private readonly IUsersService _userService;
//        private readonly IRolesService _roleService;

//        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
//        //private readonly IManyToManyRecordService _ManyToManyRecordService;

//        public UsersController(
//			IUsersService userService
//            , IRolesService roleService

//            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
//            //, IManyToManyRecordService ManyToManyRecordService
//        )
//        {
//            _userService = userService;
//            _roleService = roleService;

//            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
//            //_ManyToManyRecordService = ManyToManyRecordService;
//        }

//        // GET: Users
//        public IActionResult Index()
//        {
//            // Get collection service logic:
//            var list = _userService.Query().ToList();
//            return View(list);
//        }

//        // GET: Users/Details/5
//        public IActionResult Details(int id)
//        {
//            // Get item service logic:
//            var item = _userService.Query().SingleOrDefault(q => q.Record.Id == id);
//            return View(item);
//        }

//        protected void SetViewData()
//        {
//            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
//            ViewData["RoleId"] = new SelectList(_roleService.Query().ToList(), "Record.Id", "Name");

//            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
//            //ViewBag.ManyToManyRecordIds = new MultiSelectList(_ManyToManyRecordService.Query().ToList(), "Record.Id", "Name");
//        }

//        // GET: Users/Create
//        public IActionResult Create()
//        {
//            SetViewData();
//            return View();
//        }

//        // POST: Users/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create(UsersModel user)
//        {
//            if (ModelState.IsValid)
//            {
//                // Insert item service logic:
//                var result = _userService.Create(user.Record);
//                if (result.IsSuccessful)
//                {
//                    TempData["Message"] = result.Message;
//                    return RedirectToAction(nameof(Details), new { id = user.Record.Id });
//                }
//                ModelState.AddModelError("", result.Message);
//            }
//            SetViewData();
//            return View(user);
//        }

//        // GET: Users/Edit/5
//        public IActionResult Edit(int id)
//        {
//            // Get item to edit service logic:
//            var item = _userService.Query().SingleOrDefault(q => q.Record.Id == id);
//            SetViewData();
//            return View(item);
//        }

//        // POST: Users/Edit
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Edit(UsersModel user)
//        {
//            if (ModelState.IsValid)
//            {
//                // Update item service logic:
//                var result = _userService.Update(user.Record);
//                if (result.IsSuccessful)
//                {
//                    TempData["Message"] = result.Message;
//                    return RedirectToAction(nameof(Details), new { id = user.Record.Id });
//                }
//                ModelState.AddModelError("", result.Message);
//            }
//            SetViewData();
//            return View(user);
//        }

//        // GET: Users/Delete/5
//        public IActionResult Delete(int id)
//        {
//            // Get item to delete service logic:
//            var item = _userService.Query().SingleOrDefault(q => q.Record.Id == id);
//            return View(item);
//        }

//        // POST: Users/Delete
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public IActionResult DeleteConfirmed(int id)
//        {
//            // Delete item service logic:
//            var result = _userService.Delete(id);
//            TempData["Message"] = result.Message;
//            return RedirectToAction(nameof(Index));
//        }
//	}
//}
using BLL.Controllers.Bases;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MVC.Controllers
{
    public class UsersController : MvcController
    {
        private readonly IService<User, UsersModel> _userService;

        public UsersController(IService<User, UsersModel> userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(UsersModel user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var userModel = _userService.Query().SingleOrDefault(u => u.Record.UserName == user.Record.UserName &&
        //            u.Record.Password == user.Record.Password && u.Record.IsActive);
        //        if (userModel is not null)
        //        {
        //            List<Claim> claims = new List<Claim>()
        //            {
        //                new Claim(ClaimTypes.Name, userModel.UserName),
        //                new Claim(ClaimTypes.Role, userModel.RoleId),
        //                new Claim("Id", userModel.Record.Id.ToString())
        //            };
        //            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //            var principal = new ClaimsPrincipal(identity);
        //            await HttpContext.SignInAsync(principal, new AuthenticationProperties()
        //            {
        //                IsPersistent = true
        //            });
        //            return RedirectToAction("Index", "Home");
        //        }
        //    }
        //    return View();
        //}
        public async Task<IActionResult> Login(UsersModel user)
        {
            if (ModelState.IsValid)
            {
                var userModel = _userService.Query().SingleOrDefault(u => u.Record.UserName == user.Record.UserName &&
                    u.Record.Password == user.Record.Password && u.Record.IsActive);
                if (userModel is not null)
                {
                    List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userModel.UserName),
                new Claim(ClaimTypes.Role, userModel.RoleId),
                new Claim("Id", userModel.Record.Id.ToString())
            };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principal, new AuthenticationProperties()
                    {
                        IsPersistent = true
                    });
                    return RedirectToAction("Index", "Home");
                }
            }

            ViewData["LoginError"] = "Invalid username or password. Please try again.";
            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}