namespace BLL.DAL
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}