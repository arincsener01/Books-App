using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}