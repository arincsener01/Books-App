using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BLL.Models;
using BLL.Services;
using System.Collections.Generic;
using BLL.Services.Bases;

namespace MVC.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        const string SESSIONKEY = "Favorites";

        private readonly HttpServiceBase _httpService;
        private readonly IBooksService _bookService;

        public FavoritesController(HttpServiceBase httpService, IBooksService bookService)
        {
            _httpService = httpService;
            _bookService = bookService;
        }

        private int GetUserId() => Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == "Id")?.Value);

        private List<FavoritesModel> GetSession(int userId)
        {
            var favorites = _httpService.GetSession<List<FavoritesModel>>(SESSIONKEY);
            return favorites?.Where(f => f.UserId == userId).ToList();
        }

        public IActionResult Get()
        {
            return View("List", GetSession(GetUserId()));
        }

        public IActionResult Remove(int bookId)
        {
            var favorites = GetSession(GetUserId());
            if (favorites != null)
            {
                var favoritesItem = favorites.FirstOrDefault(c => c.BookId == bookId);
                if (favoritesItem != null)
                {
                    favorites.Remove(favoritesItem);
                    _httpService.SetSession(SESSIONKEY, favorites);
                }
            }
            return RedirectToAction(nameof(Get));
        }

        public IActionResult Add(int bookId)
        {
            int userId = GetUserId();
            var favorites = GetSession(userId) ?? new List<FavoritesModel>();
            if (!favorites.Any(f => f.BookId == bookId))
            {
                var book = _bookService.Query().SingleOrDefault(b => b.Record.Id == bookId);
                if (book != null)
                {
                    var favoritesItem = new FavoritesModel()
                    {
                        BookId = bookId,
                        UserId = userId,
                        BookName = book.Record.Name
                    };
                    favorites.Add(favoritesItem);
                    _httpService.SetSession(SESSIONKEY, favorites);
                    TempData["Message"] = $"\"{book.Record.Name}\" added to favorites.";
                }
            }
            return RedirectToAction("Index", "Books");
        }
    }
}
