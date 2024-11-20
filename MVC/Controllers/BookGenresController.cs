using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class BookGenresController : MvcController
    {
        // Service injections:
        private readonly IBookGenresService _bookGenreService;
        private readonly IGenresService _genreService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //private readonly IManyToManyRecordService _ManyToManyRecordService;

        public BookGenresController(
			IBookGenresService bookGenreService
            , IGenresService genreService

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //, IManyToManyRecordService ManyToManyRecordService
        )
        {
            _bookGenreService = bookGenreService;
            _genreService = genreService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //_ManyToManyRecordService = ManyToManyRecordService;
        }

        // GET: BookGenres
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _bookGenreService.Query().ToList();
            return View(list);
        }

        // GET: BookGenres/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _bookGenreService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            ViewData["GenreId"] = new SelectList(_genreService.Query().ToList(), "Record.Id", "Name");
            
            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //ViewBag.ManyToManyRecordIds = new MultiSelectList(_ManyToManyRecordService.Query().ToList(), "Record.Id", "Name");
        }

        // GET: BookGenres/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: BookGenres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookGenresModel bookGenre)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _bookGenreService.Create(bookGenre.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = bookGenre.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(bookGenre);
        }

        // GET: BookGenres/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _bookGenreService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: BookGenres/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookGenresModel bookGenre)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _bookGenreService.Update(bookGenre.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = bookGenre.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(bookGenre);
        }

        // GET: BookGenres/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _bookGenreService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: BookGenres/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _bookGenreService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
