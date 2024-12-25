using System.ComponentModel;

namespace BLL.Models
{
    public class FavoritesModel
    {
        public int BookId { get; set; }
        public int UserId { get; set; }

        [DisplayName("Book Name")]
        public string BookName { get; set; }
    }
}