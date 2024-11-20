using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(75)]
        public string Name { get; set; }
        public short? NumberOfPages { get; set; }
        public DateTime? PublishDate { get; set; }
        [Precision(18, 2)]
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        public bool IsTopSeller { get; set; }
        [Required(ErrorMessage = "Author is required")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public List<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}
