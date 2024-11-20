using BLL.DAL;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class BookGenresModel
    {
        public BookGenre Record { get; set; }
        [Display(Name = "Book")]
        public string BookName => Record.Book?.Name;
        [Display(Name = "Genre")]
        public string GenreName => Record.Genre?.Name;
        public int BookId => Record.BookId;
        public int GenreId => Record.GenreId;

    }
}
