using BLL.DAL;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class BooksModel
    {
        public Book Record { get; set; }
        public string Name => Record.Name;
        [Display(Name = "Number of Pages")]
        public string NumberOfPages => Record.NumberOfPages.HasValue ? Record.NumberOfPages.ToString() : "N/A";
        [Display(Name = "Publish Date")]
        public string PublishDate => Record.PublishDate is null ? string.Empty : Record.PublishDate.Value.ToString("MM/dd/yyyy");
        public string Price => Record.Price.ToString("C", new System.Globalization.CultureInfo("tr-TR"));
        [Display(Name = "Top Seller")]
        public string IsTopSeller => Record.IsTopSeller ? "Yes" : "No";
        [Display(Name = "Author")]
        public string AuthorName => $"{Record.Author?.Name} {Record.Author?.Surname}".Trim();
        //before the string.Join and Break, it was giving an error
        public string Genres => string.Join("<br>", Record.BookGenres?.Select(po => po.Genre?.Name));
        [Display(Name = "Genres")]
        public List<int> GenreIds { get => Record.BookGenres?.Select(bo => bo.GenreId).ToList(); set => Record.BookGenres = value.Select(v => new BookGenre() { GenreId = v }).ToList(); }
    }
}
